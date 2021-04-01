using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Linq;
using UnityEngine;

public class PlayfabFriendController : MonoBehaviour
{
    public static PlayfabFriendController manage;
    public static Action<List<FriendInfo>> OnFrinedListUpdated = delegate { };
    private List<FriendInfo> friends; 
    private void Awake()
    {
        manage = this;
    }

    public void PlayFabContrOnEnter()
    {
        friends = new List<FriendInfo>();
        MainMenuManager.GetPhotonFriends += HandleGetFriends;
        UIAddFriends.OnAddFriend += HandleAddPlayerFriend;
        UIFriend.OnRemoveFriend += HandleRemoveFriend;
    }

    public void PlayfabFriendControllerOnExit()
    {

        UIAddFriends.OnAddFriend -= HandleAddPlayerFriend;
        UIFriend.OnRemoveFriend -= HandleRemoveFriend;
        MainMenuManager.GetPhotonFriends -= HandleGetFriends;
    }

    private void HandleGetFriends()
    {
        GetPlayfabFriends();
    }

    private void HandleRemoveFriend(string name)
    {
        string id = friends.FirstOrDefault(f => f.TitleDisplayName == name).FriendPlayFabId;
        print("remove friend"+ name + id);
        var request = new RemoveFriendRequest { FriendPlayFabId = id };
        PlayFabClientAPI.RemoveFriend(request, OnFriendRemoveSuccess, OnFailure);
        
    }

    private void OnFriendRemoveSuccess(RemoveFriendResult obj)
    {
        GetPlayfabFriends();
    }



    private void Start()
    {
        
    }

    private void HandleAddPlayerFriend(string name)
    {
        var request = new AddFriendRequest { FriendTitleDisplayName = name};
        PlayFabClientAPI.AddFriend(request, OnFriendAddedSuccess, OnFailure);
    }

    private void GetPlayfabFriends()
    {
        var request = new GetFriendsListRequest { IncludeSteamFriends = false };
        PlayFabClientAPI.GetFriendsList(request, OnFriendListSuccess, OnFailure);
    }

    private void OnFriendListSuccess(GetFriendsListResult result)
    {
        friends = result.Friends;
        OnFrinedListUpdated?.Invoke(result.Friends);
    }

    private void OnFriendAddedSuccess(AddFriendResult result)
    {
        
        GetPlayfabFriends();
    }

    private void OnFailure(PlayFabError error)
    {
        print("Error when adding to friend" + error.GenerateErrorReport());
    }
}
