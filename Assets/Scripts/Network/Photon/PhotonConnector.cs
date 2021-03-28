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
    private RoomInfo roomInfo;
    [Header("Меню")]
    public Button Play;
    public GameObject CurrentRoomActive;
    [Header("Элементы")]
    public Slider maxPlayers;
    public Slider rangPlayers;
    public InputField roomName;
    public Text maxPlayerNum;
    public Text rangNum;
    [Header("Созданная комната")]
    public Text rangCurrentRoom;
    public Text nameCurrentRoom;
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
        PhotonNetwork.NickName = PlayerPrefs.GetString("Nickname");
    }
    #region Unity Method
    private void Start()
    {
        
        
      // if (PlayerPrefs.GetInt("FirstActive")==0)
      //  {
            //PhotonNetwork.NickName = "Player" + Random.Range(10, 9999);
            //PlayerPrefs.SetString("Nickname", PhotonNetwork.NickName);
       // }

        
        //PlayerPrefs.SetInt("FirstActive", PlayerPrefs.GetInt("FirstActive") + 1);
        
        
        Debug.Log("Connect to photon as :" + PhotonNetwork.NickName);
        PhotonNetwork.AuthValues = new AuthenticationValues(PhotonNetwork.NickName);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.ConnectUsingSettings();
        print(PlayerPrefs.GetInt("FirstActive"));
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
        ro.CustomRoomProperties = RoomCustomProps;
        PhotonNetwork.JoinOrCreateRoom(roomName.text, ro, TypedLobby.Default);
    }
   
    #endregion

    #region Public Method

    

    public override void OnJoinedLobby()
    {
        Debug.Log("You have connected to a Photon Lobby");
       // CreatePhotonRoom("Lobby");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room success name: " + PhotonNetwork.CurrentRoom.Name);
        MainMenuManager.manage.isCreateRoomPanelOpen = false;
        MainMenuManager.manage._createroom.SetActive(false);
        CurrentRoomActive.SetActive(true);
        rangCurrentRoom.text = PhotonNetwork.CurrentRoom.CustomProperties["RANG"].ToString();
        nameCurrentRoom.text = roomName.text;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Join to room name: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("You have left a room");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("You failed to join a room" + message);
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["RANG"].ToString());
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Another player has joined room" + newPlayer.UserId);
        //PlayerPrefs.SetInt("currplayers", PhotonNetwork.PlayerList.Length);
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
