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
    [SerializeField] private float refreshCoolDown;
    [SerializeField] private float refreshCountDown;
    [SerializeField] private List<PlayfabFriendInfo> friendList;
    public static PhotonFriendsController manage;
    public static Action<List<PhotonFriendInfo>> OnDisplayFriends = delegate { };
    private void Awake()
    {
        manage = this;
    }

    private void Update()
    {
     //  if (refreshCountDown > 0)
     //   {
      //      refreshCountDown -= Time.deltaTime;
       // } else
     //   {
      //      refreshCountDown = refreshCoolDown;
      //      if (PhotonNetwork.InRoom) return;
     //       FindPhotonFriends(friendList);
     //   }
    }

    private static void FindPhotonFriends(List<PlayfabFriendInfo> friends)
    {
        Debug.Log($"Handle getting Photon friends {friends.Count}");
        if (friends.Count != 0)
        {
            string[] friendDisplayNames = friends.Select(f => f.TitleDisplayName).ToArray();
            PhotonNetwork.FindFriends(friendDisplayNames);
        }
        else
        {
            List<PhotonFriendInfo> friendList = new List<PhotonFriendInfo>();
            OnDisplayFriends?.Invoke(friendList);
        }
    }

    public override void OnFriendListUpdate(List<PhotonFriendInfo> friendList)
    {
        Debug.Log($"Invoke UI to display Photon friends found: {friendList.Count}");
        OnDisplayFriends?.Invoke(friendList);
    }



    public void PhotonFriendControllerOnEnter()
    {
        PlayfabFriendController.OnFrinedListUpdated += HandleFriendsUpdated;
    }

    public void PhotonFriendsControllerOnExit()
    {

        PlayfabFriendController.OnFrinedListUpdated -= HandleFriendsUpdated;
    }
    private void HandleFriendsUpdated(List<PlayfabFriendInfo> friends)
    {
        friendList = friends;
        FindPhotonFriends(friendList);
    }
}
