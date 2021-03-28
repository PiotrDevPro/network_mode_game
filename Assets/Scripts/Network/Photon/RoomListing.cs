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
    private Text _textCurrPlayer;
    [SerializeField]
    private Text _textRoomName;
    [SerializeField]
    private Text _textRangPlayer;
    public Slider _playersCountProgress;

    public RoomInfo RoomInfo { get; private set; }
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        _playersCountProgress.minValue = 0;
        _playersCountProgress.maxValue = roomInfo.MaxPlayers;
        _textCurrPlayer.text = roomInfo.PlayerCount.ToString();
        _textMaxPlayer.text = roomInfo.MaxPlayers.ToString();
        _textRangPlayer.text = PhotonNetwork.CurrentRoom.CustomProperties["RANG"].ToString();
        _textRoomName.text = roomInfo.Name;
        _playersCountProgress.value = _playersCountProgress.maxValue - (_playersCountProgress.maxValue - roomInfo.PlayerCount);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print(PhotonNetwork.CurrentRoom.CustomProperties["RANG"].ToString());
    }

}
