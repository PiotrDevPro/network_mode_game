using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListingMenu : MonoBehaviourPunCallbacks
{

    [SerializeField] private Transform _content;
    [SerializeField] private RoomListing _roomListing;

    private List<RoomListing> _listing = new List<RoomListing>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
       // foreach (RoomInfo info in roomList)
       //{
            // Удаление из листа
          //  if (info.RemovedFromList)
           // {
              //  int index = _listing.FindIndex(x => x.RoomInfo.Name == info.Name);
             ///   if  (index != -1)
             //   {
               //     Destroy(_listing[index].gameObject);
                //    _listing.RemoveAt(index);

              //  }
      //  }
      //   else
      //  {
        //    RoomListing listing = Instantiate(_roomListing, _content);
        //    if (listing != null)
         //   {
          //      listing.SetRoomInfo(info);
            //    _listing.Add(listing);
           // }
                
       // }
            
      //  }
    }
         
}
