using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayersListing : MonoBehaviour
{
    [SerializeField]
    private Text _name;


    public Player Player { get; private set; }

    public void SetPlayerInfo(Player player)
    {
        Player = player;
        _name.text = player.NickName;
    }
}
