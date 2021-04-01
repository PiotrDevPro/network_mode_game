using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisplayFriends : MonoBehaviour
{
    public static UIDisplayFriends manage;
    [SerializeField] private Transform friendContainer;
    [SerializeField] private UIFriend uiFriendPrefab;
    private void Awake()
    {
        manage = this;
    }

    public void FriendListUpdateOnEnter()
    {

        PhotonFriendsController.OnDisplayFriends += HandleDisplayFriends;
    }

    public void UIDisplayFriendsOnExit()
    {

        PhotonFriendsController.OnDisplayFriends -= HandleDisplayFriends;
    }

    public void HandleDisplayFriends(List<FriendInfo> friends)
    {
        foreach (Transform child in friendContainer)
        {
            Destroy(child.gameObject);
        }
        foreach (FriendInfo friend in friends)
        {
            UIFriend uifriend = Instantiate(uiFriendPrefab,friendContainer);
            uifriend.Initialize(friend);
        }
    }


}
