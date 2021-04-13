using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayersItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMPro.TextMeshProUGUI nickName; 
    public Player player;
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

    public void OnClick_()
    {
        print(player.NickName);
        print(player.CustomProperties["Role"]);
        PhotonConnector.manage.currPlayersInRoom.text = PhotonNetwork.PlayerList.Length.ToString();
        //OnPlayerLeftRoom(player);
        //PhotonNetwork.LeaveRoom();
        
    }
}
