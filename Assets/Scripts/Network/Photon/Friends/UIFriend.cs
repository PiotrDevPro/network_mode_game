using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class UIFriend : MonoBehaviour
{
    public static UIFriend manage;
    [SerializeField] private TMP_Text friendNameText;
    public Text friendPrivateRoomName;
    [SerializeField] private FriendInfo friend;
    [SerializeField] private Color onlineColor;
    [SerializeField] private Color offlineColor;
    [SerializeField] private Image onlineImg;
    public bool isActive = false;


    public static Action<string> OnRemoveFriend = delegate { };
    public static Action<string> OnInviteFriend = delegate { };

    private void Awake()
    {
        manage = this;
    }

    public void Initialize(FriendInfo friend)
    {
        Debug.Log(friend.UserId + "is Online: "+ friend.IsOnline + "in room: " + friend.IsInRoom + "room name: " + friend.Room);
        this.friend = friend;
        SetupUI();
    }

    private void SetupUI()
    {
        friendNameText.SetText(this.friend.UserId);
        if (friend.IsOnline)
        {
            onlineImg.color = onlineColor;
        }
        else
        {
            onlineImg.color = offlineColor;
        }
    }

    public void RemoveFriendActive()
    {
           MainMenuManager.manage.RUSure.SetActive(true);
           
    }

    public void RemoveFriends()
    {
        OnRemoveFriend?.Invoke(friend.UserId);
        MainMenuManager.manage.RUSure.SetActive(false);
    }

    public void InviteFriend()
    {
        
        OnInviteFriend?.Invoke(friend.UserId);
        Debug.Log("Clicked to invate friend" + friend.UserId);
    }

    public void InvitesList()
    {
        MainMenuManager.manage.MainMenu();
        MainMenuManager.manage.InvitesList();
        UIDisplayInvates.manage.UIDisplayFriendsInvatesOnEnter();
    }

    public void FriendsPrivateRoom_OnClicked()
    {
        MainMenuManager.manage._friends.SetActive(false);
        MainMenuManager.manage.FriendMsgPanel.SetActive(true);
        MainMenuManager.manage.nameFriendInPrivateMsgRoom.text = friendNameText.text;
        ChatController.manage.Connect();
    }
}
