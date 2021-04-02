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
    [SerializeField] private FriendInfo friend;
    [SerializeField] private Color onlineColor;
    [SerializeField] private Color offlineColor;
    [SerializeField] private Image onlineImg;


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
        Debug.Log("Clicked to invate friend" + friend.UserId);
        OnInviteFriend?.Invoke(friend.UserId);
    }
}
