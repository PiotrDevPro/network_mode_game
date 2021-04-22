using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;
using Photon.Realtime;

public class PlayersItem : MonoBehaviourPunCallbacks
{
    public static PlayersItem manage;
    public Player player;
    [SerializeField] TMPro.TextMeshProUGUI nickName;
    public GameObject KilledIcon;
    public GameObject numbers;
    [SerializeField] Text counterToKill;
    [SerializeField] Text counterToKill1;
    
    [Header("Cчетчики")]
    private int score = 0;
    int counter = 0;
    int a = 0;

    public bool isKilled = false;


    private void Awake()
    {
        manage = this;
        counterToKill.enabled = false;
        numbers.SetActive(false);
        counterToKill1.enabled = false;
    }

    public void SetUp(Player _player)
    {
        player = _player;
        if (_player.IsLocal)
        {
            if (Application.systemLanguage != SystemLanguage.Russian)
            {
                nickName.text = "You";
            }
            else
            {
                nickName.text = "Вы";
            }

        }
        else
        {
            nickName.text = _player.NickName;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (player == otherPlayer)
        {
            Destroy(gameObject);
            PhotonConnector.manage.currPlayersInRoom.text = PhotonNetwork.PlayerList.Length.ToString();
            print("PlayerLeftRoom");
        }
    }
    public override void OnLeftRoom()
    {
        Destroy(gameObject);
        PhotonConnector.manage.currPlayersInRoom.text = PhotonNetwork.PlayerList.Length.ToString();
    }

    private void Update()
    {
        if (PhotonConnector.manage.isMafiaTimeToKillEnd)
        {
         //       a += 1;
         //       if(a == 1)
        //    {
                numbers.SetActive(false);
               // a += 1;
               print("numbers");
        }
     }

    public void OnClick_()
    {
        if (!player.IsLocal && PhotonConnector.manage.isMafiaTimeToKill)
        {
            if (!PhotonConnector.manage.isMafiaTimeToKillEnd)
            {

                if ((int)player.CustomProperties["Role"] == 2)
                {
                    score++;
                    print(player.NickName);
                    print(player.CustomProperties["Role"]);
                    PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Kick1", score } });
                    counterToKill.text = PhotonNetwork.CurrentRoom.CustomProperties["Kick1"].ToString();
                    counterToKill.enabled = true;
                    counterToKill1.enabled = false;
                    counter = 0;
                    
                }
                if ((int)player.CustomProperties["Role"] == 3)
                {
                    score++;
                    print(player.NickName);
                    print(player.CustomProperties["Role"]);
                    PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Kick2", score } });
                    counterToKill1.text = PhotonNetwork.CurrentRoom.CustomProperties["Kick2"].ToString();
                    counterToKill.enabled = false;
                    counterToKill1.enabled = true;
                    counter = 0;
                }
            }
        }
    }


    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        print("OnPlayerPropertiesUpdate");
    }

    void SetScoreText()
    {
        counterToKill.text = PhotonNetwork.CurrentRoom.CustomProperties["Kick1"].ToString();
        counterToKill1.text = PhotonNetwork.CurrentRoom.CustomProperties["Kick2"].ToString();

    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        print("OnRoomPropertiesUpdate");
        if ((int)player.CustomProperties["Role"] == 2)
        {
                numbers.SetActive(true);
                counterToKill.enabled = true;

            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["Kick1"] > (int)PhotonNetwork.CurrentRoom.CustomProperties["Kick2"])
            {
                
                counter += 1;
                if (counter == 1)
                {
                   
                    PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Killed", player.NickName } });
                    print("Killed " + player.NickName);
                    counter += 1;
                    PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Status", 1 } });
                    //if (PhotonConnector.manage.isMafiaTimeToKillEnd)
                    // {
                    // }
                }
            }
        }
        if ((int)player.CustomProperties["Role"] == 3)
        {
                numbers.SetActive(true);
                counterToKill1.enabled = true;
            
            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["Kick2"] > (int)PhotonNetwork.CurrentRoom.CustomProperties["Kick1"])
              {
                
                    counter += 1;
                  if (counter == 1)
                  {
                     PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Killed", player.NickName } });
                     print("Killed " + player.NickName);
                     counter += 1;
                    PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Status", 2 } });
                    //  if (PhotonConnector.manage.isMafiaTimeToKillEnd)
                    //  {
                   // }
                }

                

            }
                
        }
            SetScoreText();
    }

   public void KillIcon()
    {
        KilledIcon.SetActive(true);
    }
}

