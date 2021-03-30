using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayersItem : MonoBehaviourPunCallbacks
{
    [SerializeField] Text nickName; 
    Player player;
    public void SetUp(Player _player)
    {
        player = _player;
        nickName.text = _player.NickName;

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
            PhotonConnector.manage.currPlayersInRoom.text = PhotonNetwork.PlayerList.Length.ToString();
        }
    }
    public override void OnLeftRoom()
    {
        Destroy(gameObject);
        PhotonConnector.manage.currPlayersInRoom.text = PhotonNetwork.PlayerList.Length.ToString();
    }
}
