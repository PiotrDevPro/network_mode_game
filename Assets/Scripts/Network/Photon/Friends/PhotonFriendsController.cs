using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PlayfabFriendInfo = PlayFab.ClientModels.FriendInfo;
using PhotonFriendInfo = Photon.Realtime.FriendInfo;
using System;
using System.Linq;

public class PhotonFriendsController : MonoBehaviourPunCallbacks
{
    public static PhotonFriendsController manage;
    public static Action<List<PhotonFriendInfo>> OnDisplayFriends = delegate { };
    private void Awake()
    {
        manage = this;
    }

    public void PhotonFriendControllerOnEnter()
    {
        PlayfabFriendController.OnFrinedListUpdated += HandleFriendsUpdated;
    }

    public void PhotonFriendsControllerOnExit()
    {

        PlayfabFriendController.OnFrinedListUpdated -= HandleFriendsUpdated;
        print("PhotonFriendsControllerOnExit");
    }
    private void HandleFriendsUpdated(List<PlayfabFriendInfo> friends)
    {
       if (friends.Count != 0)
        {
            string[] friendDisplayNames = friends.Select(f => f.TitleDisplayName).ToArray();
            PhotonNetwork.FindFriends(friendDisplayNames);
        }
        else
        {
            List<PhotonFriendInfo> friendslist = new List<PhotonFriendInfo>();
            OnDisplayFriends?.Invoke(friendslist);
        }
    }

    public override void OnFriendListUpdate(List<PhotonFriendInfo> friendList)
    {
        OnDisplayFriends?.Invoke(friendList);
        
    }
}
