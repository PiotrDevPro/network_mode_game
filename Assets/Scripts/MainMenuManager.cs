using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager manage; //singleton
    public static Action GetPhotonFriends = delegate { };
    [Header("Менюшки")]
    public GameObject _play;
    public GameObject _options;
    public GameObject _friends;
    public GameObject _mainmenu;
    public GameObject _createroom;
    public GameObject _rules;
    public GameObject _scrollPanel;
    public GameObject _currentRoom_main;
    public GameObject _currentRoom_question;
    public GameObject _profile;
    public GameObject _loginEdit;
    public GameObject RUSure;
    public GameObject InviteFriendsPanel;
    public GameObject FriendMsgPanel;
    [Header("Профиль")]
    public GameObject friendsPanel;
    public GameObject invatesPanel;
    [Header("Настройка")]
    public GameObject _support;
    public Toggle musicOnOff;

    public Text nameFriendInPrivateMsgRoom;
    public TMPro.TextMeshProUGUI namePlayerlabel;
    [Header("Булевые переменные")]
    public bool isCreateRoomPanelOpen = false;
    public bool isPlayLobbyOpen = false;
    public bool isProfileOpened = false;
    public bool isDeleteFiles = false;

    void Awake()
    {
        manage = this;
        Application.targetFrameRate = 1000;
        musicOnOff.isOn = (PlayerPrefs.GetInt("Sound") == 0) ? true : false;
    }

    void Start()
    {
        //AudioListener.pause = (PlayerPrefs.GetInt("Sound") == 1) ? true : false;
    }

    void Update()
    {
        //PlayerPrefs.DeleteAll();
        //print(PlayerPrefs.GetString("photonroom"));
    }

    #region Profile

    public void Profile()
    {
        _profile.SetActive(true);
        namePlayerlabel.text = PlayerPrefs.GetString("Nickname");
        
        FriendControllerOnEnter();
        isProfileOpened = true;
    }

    public void MainMenu()
    {
        FriendControllerOnExit();
        _friends.SetActive(false);
        _mainmenu.SetActive(true);
        _scrollPanel.GetComponentInChildren<ScrollRect>().enabled = true;
    }

    public void Friends()
    {
        _friends.SetActive(true);
        FriendControllerOnEnter();
        Debug.Log("FriendsOpen");
        _scrollPanel.GetComponentInChildren<ScrollRect>().enabled = false;
    }

    public void ProfileExit()
    {
        
        _profile.SetActive(false);
        isProfileOpened = false;
        FriendControllerOnExit();
        UIInvate.OnRoomInvateAccept -= HandleRoomInviteAccept;
        UIDisplayInvates.manage.UIDisplayFriendsInvatesOnExit();
        ChatController.manage.Disconnect();
    }

    void FriendControllerOnEnter()
    {
        PlayfabFriendController.manage.PlayFabContrOnEnter();
        PhotonFriendsController.manage.PhotonFriendControllerOnEnter();
        UIDisplayFriends.manage.FriendListUpdateOnEnter();
        GetPhotonFriends?.Invoke();
    }

    void FriendControllerOnExit()
    {
        PlayfabFriendController.manage.PlayfabFriendControllerOnExit();
        PhotonFriendsController.manage.PhotonFriendsControllerOnExit();
        UIDisplayFriends.manage.UIDisplayFriendsOnExit();
    }

    public void Edit()
    {
        _loginEdit.SetActive(true);
    }

    public void DeleteIsOk()
    {
        UIFriend.manage.RemoveFriends();
    }

    public void DeleteIsCancel()
    {
        RUSure.SetActive(false);
        isDeleteFiles = false;
    }

    #region Invates

    public void FriendScrollActive()
    {
        friendsPanel.SetActive(true);
        invatesPanel.SetActive(false);
        FriendControllerOnEnter();
    }

    public void InvatesScrollActive()
    {
        friendsPanel.SetActive(false);
        invatesPanel.SetActive(true);
        FriendControllerOnExit();
        UIInvate.OnRoomInvateAccept += HandleRoomInviteAccept;
        UIDisplayInvates.manage.UIDisplayFriendsInvatesOnEnter();
        
    }

    public void InvitesList()
    {
        Profile();
        InvatesScrollActive();
    }

    public void InviteFriendsOpen()
    {
        InviteFriendsPanel.SetActive(true);
       
        //PhotonNetwork.LeaveRoom();
        FriendControllerOnEnter();
       // UIDisplayInvates.manage.UIDisplayFriendsInvatesOnEnter();
    }

    public void InviteFriendsClose()
    {
        InviteFriendsPanel.SetActive(false);
        //PhotonConnector.manage.JoinRoom(info);
        FriendControllerOnExit();
      //  UIDisplayInvates.manage.UIDisplayFriendsInvatesOnExit();
    }

    private void HandleRoomInviteAccept(string roomName)
    {
        PlayerPrefs.SetString("photonroom", roomName);
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();

        } else
        {
            if (PhotonNetwork.InLobby)
            {
                JoinPlayRoom();
            }
        }
    }

    public void JoinPlayRoom()
    {
        string roomName = PlayerPrefs.GetString("photonroom");
        PlayerPrefs.SetString("photonroom", "");
        PhotonNetwork.JoinRoom(roomName);

    }


    #endregion

    #endregion

    #region MainMenu


    public void PlayPanel()
    {
        _play.SetActive(true);
    }
    public void PlayPanelQuit()
    {
        _play.SetActive(false);
    }
    public void Options()
    {
        _options.SetActive(true);
    }
    public void OptionsQuit()
    {
        _options.SetActive(false);
    }

    public void CreateRoomPanell()
    {
        _createroom.SetActive(true);
        isCreateRoomPanelOpen = true;
        PhotonConnector.manage.roomName.text = "";
        PhotonConnector.manage.maxPlayers.maxValue = 21;
        PhotonConnector.manage.maxPlayers.minValue = 2;
        PhotonConnector.manage.rangPlayers.value = 0;
        PhotonConnector.manage.rangPlayers.minValue = 0;
        PhotonConnector.manage.rangPlayers.maxValue = 8;
    }

    public void CreateRoomPanellQuit()
    {
        _createroom.SetActive(false);
        isCreateRoomPanelOpen = false;
    }

    public void CurrentRoomChat()
    {
        _currentRoom_main.SetActive(true);
        _currentRoom_question.SetActive(false);
    }

    public void CurrentRoomQuestion()
    {
        _currentRoom_main.SetActive(false);
        _currentRoom_question.SetActive(true);
    }

    public void Rules()
    {
        _rules.SetActive(true);
    }

    public void RulesExit()
    {
        _rules.SetActive(false);
    }

    public void FriendPrivatePanel()
    {
        FriendMsgPanel.SetActive(true);
        _friends.SetActive(false);
    }

    public void FriendPrivatePanelClose()
    {
        FriendMsgPanel.SetActive(false);
        ChatController.manage.Disconnect();
    }

    #endregion

    #region Settings

    public void Support()
    {
        _support.SetActive(true);
        _options.SetActive(false);
    }

    public void SupportQuit()
    {
        _support.SetActive(false);
        _options.SetActive(true);
    }

    public void policy()
    {
        Application.OpenURL("http://google.com");
    }

    public void musiconoff(Toggle tgl)
    {
        if (tgl.isOn)
        {
            PlayerPrefs.SetInt("Sound",0);
            AudioListener.pause = false;
        } 
        else
        {
            PlayerPrefs.SetInt("Sound",1);
            AudioListener.pause = true;
        }
    }

    #endregion

    #region Network 



    #endregion

}
