using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;

public class KilliconActive : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform PlayerList;

    private void Start()
    {


       
        
    }

    private void Update()
    {
        if (PhotonConnector.manage.isMafiaTimeToKillEnd)
        {
            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["Kill"] == 1)
            {
                // foreach (Transform obj in PlayerList)
                //{

                PlayerList.GetChild(1).gameObject.SetActive(false);

               // }

            }

            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["Kill"] == 2)
            {
                //  foreach (Transform obj in PlayerList)
                // {

                PlayerList.GetChild(2).gameObject.SetActive(false);

                // }
            }
        }
    }
}
