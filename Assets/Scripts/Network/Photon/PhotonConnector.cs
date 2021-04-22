using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;

public class PhotonConnector : MonoBehaviourPunCallbacks
{
    public static PhotonConnector manage;
    public Player Player { get; private set; }
    private ExitGames.Client.Photon.Hashtable playerCustomProperties = new ExitGames.Client.Photon.Hashtable();
    [Header("Меню")]
    public Button Play;
    public Button CreateRoom;
    public GameObject CurrentRoomActive;
    public GameObject loginPanel;
    public GameObject invatesPanel;
    public GameObject PlayRoom;

    [Header("Элементы")]
    public Slider maxPlayers;
    public Slider rangPlayers;
    public InputField roomName;
    public Text maxPlayerNum;
    public Text rangNum;

    [Header("Созданная комната")]
    public Text rangCurrentRoom;
    public Text nameCurrentRoom;
    public int rangNumbers;
    public Text currPlayersInRoom;
    public Text maxPlayersInRoom;
    public Text Counter;
    public Text gameCounter;
    private float curr = 0;
    private float starttime = 20f;// 20
    private int counter = 0;

    [Header("Элементы Гэймплэя")]
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playersListContent;
    [SerializeField] GameObject playersListItemPrefab;
    [SerializeField] GameObject PlayersListForVote;
    [SerializeField] Transform GameContent;
    [SerializeField] Transform GameContentPlayersList;
    [SerializeField] GameObject dayPrefimg;
    [SerializeField] GameObject nightPrefimg;
    [SerializeField] GameObject mafiaTimetoKillPref;
    [SerializeField] GameObject resultOfKillPref;

    [Header("Элементы GameToolBar")]
    [SerializeField] GameObject PlayMenuPanel;
    [SerializeField] GameObject dayImg;
    [SerializeField] GameObject nightImg;
    [SerializeField] GameObject voteImg;
    [SerializeField] GameObject mafPref;
    [SerializeField] GameObject mirPref;
    [SerializeField] Transform mafConteiner;
    [SerializeField] Transform mirContainer;
    
    [Header("Булевые переменные GAMEPLAY")]
    public bool isTimeStart = false;
    public bool isStartGame = false;
    public bool isDayStart = false;
    public bool isDayEnd = false;
    public bool isNightStart = false;
    public bool isNightEnd = false;
    public bool isMafiaTimeToKill = false;
    public bool isMafiaTimeToKillEnd = false;
    public bool isStartMsgResultKill = false;

    [Header("Булевые переменные КОМНАТА")]
    public bool isNotEnougPlayersInRoom = false;
    public bool isRoomCreatedAndJoined = false;
    public bool isLastPlayerConnected = false;
    public bool isConected = false;

    public Player player_;

    private void Update()
    {
        //print(isTimeStart);
        //PlayerPrefs.DeleteAll();
        if (MainMenuManager.manage.isCreateRoomPanelOpen)
        {
            maxPlayerNum.text = ((int)maxPlayers.value).ToString();
            rangNum.text = ((int)rangPlayers.value).ToString();
        }

        #region GamePlay
        if (isRoomCreatedAndJoined && isTimeStart)
        {
            Timer();
            if (Application.systemLanguage != SystemLanguage.Russian)
            {
                Counter.text = "The game will start after " + curr.ToString("0");
            }
            else
            {
                Counter.text = "Игра начнется через " + curr.ToString("0");
            }

        }
        else if (isRoomCreatedAndJoined && !isTimeStart || isNotEnougPlayersInRoom)
        {
            if (Application.systemLanguage != SystemLanguage.Russian)
            {
                Counter.text = "Recruitment in progress";
            }
            else
            {
                Counter.text = "Идет набор";
            }
            
        }
        if (isLastPlayerConnected)
        {
            
            Timer();
            if (Application.systemLanguage != SystemLanguage.Russian)
            {
                Counter.text = "The game will start after " + curr.ToString("0");
            }
            else
            {
                Counter.text = "Игра начнется через " + curr.ToString("0");
            }
        }
        if (isStartGame)
        {
            Counter.text = "";
            if (!isNotEnougPlayersInRoom)
            {
                Timer();
                gameCounter.text = curr.ToString("0");
                
                if (isNightStart && !isDayStart)
                {
                    gameCounter.text = curr.ToString("0");
                    
                }
            }
            
        }

        #endregion

    }

    private void Awake()
    {
        manage = this;
        if (PlayerPrefs.GetInt("FirstActive") == 0)
        {
            PhotonNetwork.NickName = "Player " + UnityEngine.Random.Range(10, 9999);
            PlayerPrefs.SetString("Nickname", PhotonNetwork.NickName);
        }
        
    }
    #region Unity Method
    private void Start()
    {
        OnMasterConnect();
        PlayerPrefs.SetInt("ischatconnected", 0);
        curr = starttime;
        isStartGame = false;
    }

    public void OnMasterConnect()
    {
        PlayFabLogin.manage.username = PlayerPrefs.GetString("Nickname");

        PlayerPrefs.SetInt("FirstActive", PlayerPrefs.GetInt("FirstActive") + 1);
        PhotonNetwork.NickName = PlayerPrefs.GetString("Nickname");
        Debug.Log("Connect to photon as :" + PhotonNetwork.NickName);
        PhotonNetwork.AuthValues = new AuthenticationValues(PhotonNetwork.NickName);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.ConnectUsingSettings();
        PlayFabLogin.manage.Login();
        print(PhotonNetwork.NickName);
        maxPlayers.maxValue = 21;
        maxPlayers.minValue = 2;
        rangPlayers.minValue = 0;
        rangPlayers.maxValue = 8;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("ConnectedToMaster");
        Play.interactable = true;
        CreateRoom.interactable = true;
        isConected = true;
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }


    public void CreatePhotonRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (roomName.text == "")
        {
            roomName.text = "Room" + UnityEngine.Random.Range(1, 1000);
        }
        maxPlayers.minValue = 0;
        curr = starttime;
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = (byte)maxPlayers.value;
        Hashtable RoomCustomProps = new Hashtable();
        RoomCustomProps.Add("RANG", (int)rangPlayers.value);
        RoomCustomProps.Add("MAXP", (int)maxPlayers.value);
        RoomCustomProps.Add("Kick1", 0);
        RoomCustomProps.Add("Kick2", 0);
        ro.CustomRoomProperties = RoomCustomProps;
        PhotonNetwork.JoinOrCreateRoom(roomName.text, ro, TypedLobby.Default);
    }

    public void LeftRoom()
    {
        PhotonNetwork.LeaveRoom();
        isLastPlayerConnected = false;
        isRoomCreatedAndJoined = false;
        isNightStart = false;
        isStartGame = false;
        isTimeStart = false;
        curr = 20;
        PlayRoom.SetActive(false);
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);

    }

    public void TimerWhenReady(RoomInfo info)
    {
        if (info.MaxPlayers == info.MaxPlayers)
        {
            isTimeStart = true;
        }
    }

    #endregion

    #region Game

   

    void Day()
    {
        if (isStartGame)
        {
            PlayMenuPanel.SetActive(false);
            dayImg.SetActive(true);
            nightImg.SetActive(false);
                Player[] playerz = PhotonNetwork.PlayerList;
                for (int i = 0; i < playerz.Length; i++)
                {
                    Instantiate(PlayersListForVote, GameContentPlayersList).GetComponent<PlayersItem>().SetUp(playerz[i]);
                }

                if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
                {
                    Instantiate(mafPref, mafConteiner);
                    Instantiate(mirPref, mirContainer);

                }
                if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
                {
                    Instantiate(mafPref, mafConteiner);
                    Instantiate(mafPref, mafConteiner);
                    Instantiate(mirPref, mirContainer);
                }
            
            
            isDayStart = true;
            curr = 60; // 60
            print(PhotonNetwork.CurrentRoom.PlayerCount);
                
        }
    }




    void Night()
    {
            isDayStart = false;
            isDayEnd = true;
            isNightStart = true;
            dayImg.SetActive(false);
            nightImg.SetActive(true);
            gameCounter.color = Color.cyan;
            curr = 25; // 25
            
    }

    void TimeTokill()
    {
        isNightStart = false;
        isNightEnd = true;
        isMafiaTimeToKill = true;
        gameCounter.color = Color.red;
        voteImg.SetActive(true);
        dayImg.SetActive(false);
        nightImg.SetActive(false);
        curr = 5; // 15
    }

    void TimeToKillEnd()
    {

        isMafiaTimeToKill = false;
        isMafiaTimeToKillEnd = true;
        isStartMsgResultKill = true;
        gameCounter.color = Color.yellow;
        voteImg.SetActive(false);
        dayImg.SetActive(true);
        nightImg.SetActive(false);
        curr = 5; //

    }

    void Day2()
    {
        isStartMsgResultKill = false;
        isDayStart = true;
        isDayEnd = true;
        dayImg.SetActive(true);
        nightImg.SetActive(false);
        curr = 60;
    }

    void Timer()
    {

        curr -= 1 * Time.deltaTime;
        if (curr <= 0 && !isStartGame)
        {

            curr = 0; 
            isLastPlayerConnected = false;
            isTimeStart = false;
            isRoomCreatedAndJoined = false;
            isStartGame = true;
            PlayRoom.SetActive(true);
            Day();
            Instantiate(dayPrefimg, GameContent);
            print("Day1");

        }

        if (curr <= 0 && !isDayEnd)
        {
            curr = 0;
            Night();
            Instantiate(nightPrefimg, GameContent);
            print("Night");

        }

        if (curr <= 0 && !isNightEnd)
        {
            curr = 0;
            TimeTokill();
            Instantiate(mafiaTimetoKillPref, GameContent);
            print("TimeTokill");

        }

        if (curr <= 0 && !isMafiaTimeToKillEnd)
        {

            curr = 0;
            TimeToKillEnd();
            PlayersItem.manage.numbers.SetActive(false);
            Instantiate(resultOfKillPref, GameContent);
            print("isMafiaTimeToKillEnd");

        }

        if (curr <= 0 && !isDayStart)
        {

            curr = 0;
            Day2();
            Instantiate(dayPrefimg, GameContent);
            print("Day2");
        }

    }

    #endregion

    public void InvitesPanelOpen()
    {
        invatesPanel.SetActive(true);
    }

    public void JoinRoomNow()
    {
        CreatePhotonRoom();
    }

    public void KickFromRoom()
    {

    }

    #region Photon Methods

    public override void OnJoinedLobby()
    {
        Debug.Log("You have connected to a Photon Lobby");
        string roomName = PlayerPrefs.GetString("photonroom");

        if (!string.IsNullOrEmpty(roomName))
        {
            MainMenuManager.manage.JoinPlayRoom();
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room success name: " + PhotonNetwork.CurrentRoom.Name);
        
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Join to room name: " + PhotonNetwork.CurrentRoom.Name);
        MainMenuManager.manage.isCreateRoomPanelOpen = false;
        MainMenuManager.manage._createroom.SetActive(false);
        if (PlayerPrefs.GetInt("ischatconnected") == 0)
        {
            ChatController.manage.Connect();
            PlayerPrefs.SetInt("ischatconnected",1);
        }
        CurrentRoomActive.SetActive(true);
        isRoomCreatedAndJoined = true;

        if (PhotonNetwork.CurrentRoom.PlayerCount != PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            isTimeStart = false;
        }
        rangCurrentRoom.text = PhotonNetwork.CurrentRoom.CustomProperties["RANG"].ToString();
        nameCurrentRoom.text = PhotonNetwork.CurrentRoom.Name.ToString();
        maxPlayersInRoom.text = PhotonNetwork.CurrentRoom.CustomProperties["MAXP"].ToString();

        Player[] playerz = PhotonNetwork.PlayerList;
        foreach (Transform child in playersListContent)
        {
            Destroy(child.gameObject);
        }


        for (int i = 0; i < playerz.Length; i++)
        {
            Instantiate(playersListItemPrefab, playersListContent).GetComponent<PlayersItem>().SetUp(playerz[i]);
            currPlayersInRoom.text = playerz.Length.ToString();
        }

        Hashtable playerCustomProps = new Hashtable();
        playerCustomProps.Add("Role", PhotonNetwork.CurrentRoom.PlayerCount +1);
        PhotonNetwork.SetPlayerCustomProperties(playerCustomProps);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("You have left a room");
        isRoomCreatedAndJoined = false;
        isStartGame = false;
        isTimeStart = false;
        isNightStart = false;
        isDayStart = false;
        isDayEnd = false;
        isMafiaTimeToKill = false;
        isMafiaTimeToKillEnd = false;
        //if ((int)Player.CustomProperties["Kick"] != 10)
        //{
         //   PhotonNetwork.LeaveRoom();
        //}
        foreach (Transform trans in GameContent)
        {
            Destroy(trans.gameObject);
        }
        foreach (Transform trans in mafConteiner)
        {
            Destroy(trans.gameObject);
        }

        foreach (Transform trans in mirContainer)
        {
            Destroy(trans.gameObject);
        }
        curr = 20;
        playerMenu.manage.closePlayerPanel();
        CurrentRoomActive.SetActive(false);

        ChatController.manage.Disconnect();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("You failed to join a room" + message);
    }
    
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        Debug.Log("OnRoomPropertiesUpdate");
        

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i =0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListing>().SetRoomInfo(roomList[i]);
            
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
        Debug.Log("Another player has joined room" + newPlayer.NickName);
        Instantiate(playersListItemPrefab, playersListContent).GetComponent<PlayersItem>().SetUp(newPlayer);
        currPlayersInRoom.text = PhotonNetwork.PlayerList.Length.ToString();
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            isLastPlayerConnected = true;
        }
        
    }



    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Another player has left room" + otherPlayer.NickName);
        isLastPlayerConnected = false;
        curr = 25f;
        print(PhotonNetwork.CurrentRoom.MaxPlayers);
        print(PhotonNetwork.CurrentRoom.PlayerCount);
        print(isNotEnougPlayersInRoom);
        if (PhotonNetwork.CurrentRoom.PlayerCount != PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            isStartGame = false;
            isTimeStart = false;
            isNightStart = false;
            isDayStart = false;
            isDayEnd = false;
            isMafiaTimeToKill = false;
            isMafiaTimeToKillEnd = false;
            print("isMafiaTimeToKillEndFalse");
            foreach (Transform trans in GameContent)
            {
                Destroy(trans.gameObject);
            }
            isNotEnougPlayersInRoom = true;
            PlayRoom.SetActive(false);
        }

    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("New master client" + newMasterClient.NickName);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconected from Srver for reason " + cause.ToString());
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Room creations failed: " + message.ToString());
    }

    #endregion




}
