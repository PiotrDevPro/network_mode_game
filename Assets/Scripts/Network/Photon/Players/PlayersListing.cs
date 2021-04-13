using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayersListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _name;

    public Player Player { get; private set; }

    public void SetPlayerInfo(Player player)
    {
        Player = player;
        _name.text = player.NickName;
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
       // if (targetPlayer != null && targetPlayer == Player)
      //  {
       //     SetPlayerText(targetPlayer);
       // }
    }

    private void SetPlayerText(Player player)
    {
        
    }
}
