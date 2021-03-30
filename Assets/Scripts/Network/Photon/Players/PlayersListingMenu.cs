using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlayersListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private PlayersListing _playerListing;

    private List<PlayersListing> _listings = new List<PlayersListing>();

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayersListing listing = Instantiate(_playerListing,_content);
        if (listing != null)
        {
            listing.SetPlayerInfo(newPlayer);
            _listings.Add(listing);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
            if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }
}
