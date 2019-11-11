﻿using Mixer.Chat.Models;
using Mixer.Chat.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Mixer.Chat
{
    public class Client
    {
        private CancellationToken mCancellationToken;
        private Uri mChatUri = null;
        private String mAuthKey = null;
        private event EventHandler<MessageEventArgs> SocketEvent;

        public event EventHandler<WelcomeEventArgs> WelcomeEvent;
        public event EventHandler<UserJoinEventArgs> UserJoinEvent;
        public event EventHandler<UserLeaveEventArgs> UserLeaveEvent;        
        public event EventHandler<ChatMessageEventArgs> ChatMessageEvent;

        private ClientWebSocket mClient = null;
        private Int32 mMessageId = 0;

        private Int64 mChannelId = 0;
        private Int64 mUserId = 0;

        private Dictionary<Int32, Action<String>> mReplyHandler = null;

        public Client()
        {
            mCancellationToken = new CancellationToken();
            mReplyHandler = new Dictionary<int, Action<string>>();
            SocketEvent += Client_SocketEvent;
        }



        public async Task ConnectWithToken(String accessToken, String channelName)
        {
            var channelInfo = await GetChannelInfo(channelName);
            await ConnectWithToken(accessToken, channelInfo.Id);
        }

        private async Task<HttpChannelInfo> GetChannelInfo(String channelName)
        {
            var url = $"https://mixer.com/api/v1/channels/{channelName}?fields=id,userId";
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                var responseBody = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var channel = JsonConvert.DeserializeObject<HttpChannelInfo>(responseBody);
                    this.mUserId = channel.UserId;
                    this.mChannelId = channel.Id;
                    return channel;
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<HttpError>(responseBody);
                    throw new Exception($"{error.Error} - {error.ErrorDesciption}");
                }
            }
        }

        public async Task ConnectWithToken(String accessToken, Int64 channelId)
        {
            var channelInfo = await GetChannelInfo(accessToken, channelId);
            if (String.IsNullOrEmpty(channelInfo.AuthKey))
            {
                throw new Exception("Cannot Retrieve AuthKey From Token");
            }
            await Connect(channelInfo.AuthKey, channelId, mUserId, channelInfo.EndPoints[0]);
        }

        private async Task<ChannelInfo> GetChannelInfo(String accessToken, Int64 channelId)
        {
            var url = $"https://mixer.com/api/v1/chats/{channelId}";
            using (var httpClient = new HttpClient())
            {
                var header = httpClient.DefaultRequestHeaders;
                header.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await httpClient.GetAsync(url);
                var responseBody = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var channelInfo = JsonConvert.DeserializeObject<ChannelInfo>(responseBody);
                    return channelInfo;
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<HttpError>(responseBody);
                    throw new Exception($"{error.Error} - {error.ErrorDesciption}");
                }
            }
        }

        public async Task Connect(String authKey, Int64 channelId, Int64 userId, String endPoint)
        {
            this.mAuthKey = authKey;
            this.mChatUri = new Uri(endPoint);
            this.mChannelId = channelId;
            this.mUserId = userId;
            await this.Connect();
        }

        private async Task Connect()
        {
            mClient = new ClientWebSocket();
            await mClient.ConnectAsync(mChatUri, mCancellationToken);
            ReceiveThread(mClient);            
        }

        private void Client_SocketEvent(object sender, MessageEventArgs e)
        {
            switch (e.Message.MessageType)
            {
                case WsMessageType.@event:
                    try
                    {
                        HandleSocketEvent(e);
                    } catch (Exception ex)
                    {
                        Console.WriteLine($"Exception - {ex.Message}");
                    }
                    break;
                case WsMessageType.reply:
                    try
                    {
                        HandleSocketReply(e);
                    } catch (Exception ex)
                    {
                        Console.WriteLine("Exception 2");
                    }
                    break;
            }
        }

        private void HandleSocketEvent(MessageEventArgs e)
        {
            var eventMessage = JsonConvert.DeserializeObject<WsEvent<Object>>(e.MessageString);
            switch (eventMessage.Event)
            {
                case WsEventType.WelcomeEvent:
                    var welcomeEvent = JsonConvert.DeserializeObject<WsEvent<WsWelcomeEvent>>(e.MessageString);
                    this.WelcomeEvent?.Invoke(this, new WelcomeEventArgs() { Server = welcomeEvent.Data.Server });
                    break;
                case WsEventType.UserJoin:
                    var userJoinEvent = JsonConvert.DeserializeObject<WsEvent<WsUserJoinEvent>>(e.MessageString);
                    this.UserJoinEvent?.Invoke(this, new UserJoinEventArgs()
                    {
                        EventData = userJoinEvent.Data
                    });
                    break;
                case WsEventType.UserLeave:
                    var userLeaveEvent = JsonConvert.DeserializeObject<WsEvent<WsUserLeaveEvent>>(e.MessageString);
                    this.UserLeaveEvent?.Invoke(this, new UserLeaveEventArgs()
                    {
                        EventData = userLeaveEvent.Data
                    });
                    break;
                case WsEventType.ChatMessage:
                    var chatMessageEvent = JsonConvert.DeserializeObject<WsEvent<WsChatMessageEvent>>(e.MessageString);
                    this.ChatMessageEvent?.Invoke(this, new ChatMessageEventArgs()
                    {
                        EventData = chatMessageEvent.Data
                    });
                    break;
            }
        }

        private void HandleSocketReply(MessageEventArgs e)
        {
            if (mReplyHandler.ContainsKey(e.MessageId))
            {
                mReplyHandler[e.MessageId](e.MessageString);
                mReplyHandler.Remove(e.MessageId);
            }
        }

        public async Task<Int32> Auth(Func<WsAuthReply, Task> callBack)
        {
            var messageId = mMessageId++;
            WsMethod method = new WsMethod()
            {
                MessageId = messageId,
                MessageType = WsMessageType.method,
                Method = "auth",
                Arguments = new object[] { this.mChannelId, this.mUserId, this.mAuthKey }
            };
            var jsonString = JsonConvert.SerializeObject(method);
            var jsonByte = System.Text.Encoding.UTF8.GetBytes(jsonString);
            mReplyHandler.Add(messageId, async message =>
            {
                var result = JsonConvert.DeserializeObject<WsReply<WsAuthReply>>(message);
                await callBack(result.Data);
            });
            await mClient.SendAsync(new ArraySegment<byte>(jsonByte), WebSocketMessageType.Text, true, mCancellationToken);


            return messageId;
        }

        public async Task<Int32> Msg(String chatMessage, Func<WsMessageReply, Task> callBack)
        {
            var messageId = mMessageId++;
            WsMethod method = new WsMethod()
            {
                MessageId = messageId,
                MessageType = WsMessageType.method,
                Method = "msg",
                Arguments = new object[] { chatMessage }
            };
            var jsonString = JsonConvert.SerializeObject(method);
            var jsonByte = System.Text.Encoding.UTF8.GetBytes(jsonString);
            mReplyHandler.Add(messageId, async message =>
            {
                var result = JsonConvert.DeserializeObject<WsReply<WsMessageReply>>(message);
                await callBack(result.Data);
            });
            await mClient.SendAsync(new ArraySegment<byte>(jsonByte), WebSocketMessageType.Text, true, mCancellationToken);


            return messageId;
        }

        private void ReceiveThread(ClientWebSocket client)
        {
            var byteBuffer = new Byte[65535];
            var buffer = new ArraySegment<Byte>(byteBuffer);
            var messageBuffer = new List<Byte>();
            WebSocketMessageType messageType = WebSocketMessageType.Text;
            _ = Task.Run(async () =>
            {
                while (!mCancellationToken.IsCancellationRequested)
                {
                    var result = await client.ReceiveAsync(buffer, mCancellationToken);
                    messageType = result.MessageType;
                    if (messageType == WebSocketMessageType.Close)
                    {
                        await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "", mCancellationToken);
                        return;
                    }

                    messageBuffer.AddRange(buffer.Array.Skip(buffer.Offset).Take(result.Count).ToArray());

                    if (result.EndOfMessage)
                    {
                        if (messageType == WebSocketMessageType.Text)
                        {
                            var messageContent = System.Text.Encoding.UTF8.GetString(messageBuffer.ToArray());

                            var message = JsonConvert.DeserializeObject<WsMessage>(messageContent);
                            if (SocketEvent != null)
                            {
                                SocketEvent(this, new MessageEventArgs()
                                {
                                    Message = message,
                                    MessageString = messageContent,
                                    MessageId = message.MessageId
                                });
                            }
                        }

                        messageBuffer.Clear();
                    }
                }
            });
        }
    }
}
