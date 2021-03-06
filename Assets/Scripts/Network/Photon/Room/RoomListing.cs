using Photon.Realtime;
using Photon.Pun;
using Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class RoomListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _textMaxPlayer;
    [SerializeField]
    private GameObject _maxPlayersInRoom;
    [SerializeField]
    private GameObject _currPlayersInRoom;
    [SerializeField]
    private Text _textCurrPlayer;
    [SerializeField]
    private Text _textRoomName;
    [SerializeField]
    private Text _textRangPlayer;
    public Slider _playersCountProgress;

    public  RoomInfo RoomInfo;
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        _playersCountProgress.minValue = 0;
        _playersCountProgress.maxValue = roomInfo.MaxPlayers;
        _textCurrPlayer.text = roomInfo.PlayerCount.ToString();
        _textMaxPlayer.text = roomInfo.MaxPlayers.ToString();
        _textRoomName.text = roomInfo.Name;
        _playersCountProgress.value = _playersCountProgress.maxValue - (_playersCountProgress.maxValue - roomInfo.PlayerCount);
        if (_playersCountProgress.value == roomInfo.MaxPlayers)
        {
            _maxPlayersInRoom.SetActive(true);
        }
        else
        {
            _maxPlayersInRoom.SetActive(false);

        }
    }

    public void OnClick_Button()
    {
        
        PhotonConnector.manage.JoinRoom(RoomInfo);
        PhotonConnector.manage.TimerWhenReady(RoomInfo);

    }

}
