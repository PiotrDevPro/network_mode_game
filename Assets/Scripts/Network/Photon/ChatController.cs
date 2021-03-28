using System.Collections;
using System.Collections.Generic;
using System;
using Photon.Pun;
using Photon.Chat;
using UnityEngine;
using ExitGames.Client.Photon;

public class ChatController : MonoBehaviour, IChatClientListener
{
    [SerializeField] private string nickName;
    private ChatClient chatClient;

    private void Awake()
    {
        nickName = PlayerPrefs.GetString("Nickname");
    }
    private void Start()
    {

            chatClient = new ChatClient(this);
            ConnectToPhotonChat();
        
    }

    private void Update()
    {

            chatClient.Service();
    }

    public void SendDirectMessage(string reciept, string msg)
    {
        chatClient.SendPrivateMessage(reciept, msg);
    }

    private void ConnectToPhotonChat()
    {
        print("Connecting to photon chat");
        chatClient.AuthValues = new AuthenticationValues(nickName);
        ChatAppSettings chatSettings = PhotonNetwork.PhotonServerSettings.AppSettings.GetChatSettings();
        chatClient.ConnectUsingSettings(chatSettings);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
       // throw new System.NotImplementedException();
    }

    public void OnDisconnected()
    {
        print("you have disconnected from the photon chat");
    }

    public void OnConnected()
    {
        print("you have connected to the photon chat");
        
    }

    public void SentMessages()
    {
        SendDirectMessage("Me", "Hello");
    }

    public void OnChatStateChange(ChatState state)
    {
        //throw new System.NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        throw new System.NotImplementedException();
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
       if (!string.IsNullOrEmpty(message.ToString()))
        {
            //format [Sender: Reciept]
            string[] splitNames = channelName.Split(new char[] { ':' });
            string senderName = splitNames[0];
            if (!sender.Equals(senderName, StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log(sender+":"+message);
            }
        }
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }
}
