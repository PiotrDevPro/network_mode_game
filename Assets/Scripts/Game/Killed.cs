using System.Collections;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Killed : MonoBehaviourPunCallbacks
{
    public static Killed manage;
    [SerializeField] TMPro.TextMeshProUGUI nickName;
    [SerializeField] Transform playersListContent;
    public Player player;

    public bool isKilled1 = false;
    public bool isKilled2 = false;

    private void Awake()
    {
        manage = this;
    }

    public void SetUp(Player _player)
    {
        player = _player;
        nickName.text =_player.NickName;
    }

    private void Start()
    {

      if  ((int)PhotonNetwork.CurrentRoom.CustomProperties["Kick1"] > (int)PhotonNetwork.CurrentRoom.CustomProperties["Kick2"])
        {
            nickName.text = PhotonNetwork.CurrentRoom.CustomProperties["Killed"].ToString();
            if  ((int)PhotonNetwork.CurrentRoom.CustomProperties["Status"] == 1)
            {
                PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Kill", 1 } });
            }
        }

        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["Kick2"] > (int)PhotonNetwork.CurrentRoom.CustomProperties["Kick1"])
        {
            nickName.text = PhotonNetwork.CurrentRoom.CustomProperties["Killed"].ToString();
            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["Status"] == 2)
            {
                PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Kill", 2 } });
            }
        }

        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["Kick2"] == (int)PhotonNetwork.CurrentRoom.CustomProperties["Kick1"])
        {
            if (Application.systemLanguage != SystemLanguage.Russian)
            {
                nickName.text = "Nobody was killed";
                
            } else
            {
                nickName.text = "Никто не пострадал";
            }
        }
        
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        print("OnRoomPropertiesUpdateKilled");
        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["Kill"] == 1)
        {
            print("Kill1");

        }

        if ((int)PhotonNetwork.CurrentRoom.CustomProperties["Kill"] == 2)
        {
            print("Kill2");

        }

    }
}
