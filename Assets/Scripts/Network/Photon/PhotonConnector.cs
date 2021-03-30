using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;

public class PhotonConnector : MonoBehaviourPunCallbacks
{
    public static PhotonConnector manage;
    [Header("Меню")]
    public Button Play;
    public Button CreateRoom;
    public GameObject CurrentRoomActive;
    public GameObject loginPanel;
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
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playersListContent;
    [SerializeField] GameObject playersListItemPrefab;
    [Header("Булевые переменные")]
    public bool isRoomCreated = false;
    public bool isConected = false;

    

    private void Update()
    {
        //PlayerPrefs.DeleteAll();
        if (MainMenuManager.manage.isCreateRoomPanelOpen)
        {
            maxPlayerNum.text = ((int)maxPlayers.value).ToString();
            rangNum.text = ((int)rangPlayers.value).ToString();
        }

    }

    private void Awake()
    {
        manage = this;
        if (PlayerPrefs.GetInt("FirstActive") == 0)
        {
            PhotonNetwork.NickName = "Player " + Random.Range(10, 9999);
            PlayerPrefs.SetString("Nickname", PhotonNetwork.NickName);
        }
        
    }
    #region Unity Method
    private void Start()
    {
        
        
       //if (PlayerPrefs.GetInt("FirstActive")==0)
       // {
            //loginPanel.SetActive(true);
       // }
        //else
        //{
            PlayFabLogin.manage.username = PlayerPrefs.GetString("Nickname");
            PlayFabLogin.manage.Login();
        //}
        
        PlayerPrefs.SetInt("FirstActive", PlayerPrefs.GetInt("FirstActive") + 1);
        PhotonNetwork.NickName = PlayerPrefs.GetString("Nickname");
        Debug.Log("Connect to photon as :" + PhotonNetwork.NickName);
        PhotonNetwork.AuthValues = new AuthenticationValues(PhotonNetwork.NickName);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.ConnectUsingSettings();
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
            roomName.text = "Room" + Random.Range(1, 1000);
        }
        maxPlayers.minValue = 0;
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = (byte)maxPlayers.value;
        Hashtable RoomCustomProps = new Hashtable();
        RoomCustomProps.Add("RANG", (int)rangPlayers.value);
        RoomCustomProps.Add("MAXP", (int)maxPlayers.value);
        ro.CustomRoomProperties = RoomCustomProps;
        
        PhotonNetwork.JoinOrCreateRoom(roomName.text, ro, TypedLobby.Default);
    }
   
    #endregion

    #region Public Method

    public void LeftRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);

    }


    public override void OnJoinedLobby()
    {
        Debug.Log("You have connected to a Photon Lobby");
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
        ChatController.manage.Connect();
        CurrentRoomActive.SetActive(true);
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
    }

    public override void OnLeftRoom()
    {
        Debug.Log("You have left a room");
        
        
        playerMenu.manage.closePlayerPanel();
        CurrentRoomActive.SetActive(false);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("You failed to join a room" + message);
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["RANG"].ToString());
        
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
        Debug.Log("Another player has joined room" + newPlayer.UserId);
        Instantiate(playersListItemPrefab, playersListContent).GetComponent<PlayersItem>().SetUp(newPlayer);
        currPlayersInRoom.text = PhotonNetwork.PlayerList.Length.ToString();

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Another player has joined room" + otherPlayer.UserId);
        
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("New master client" + newMasterClient.UserId);
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
