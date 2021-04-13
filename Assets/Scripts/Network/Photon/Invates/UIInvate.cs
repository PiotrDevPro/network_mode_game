using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInvate : MonoBehaviour
{

    [SerializeField] private string _friendName;
    [SerializeField] private string _roomName;
    [SerializeField] private TMP_Text friendNameText;

    public static Action<UIInvate> OnInvateAccept = delegate { };
    public static Action<string> OnRoomInvateAccept = delegate { };
    public static Action<UIInvate> OnInvateDecline = delegate { };
    public void Initialize(string friendName, string roomName)
    {
        _friendName = friendName;
        _roomName = roomName;
        friendNameText.SetText(_friendName);
    }

    public void AcceptInvate()
    {
        OnInvateAccept?.Invoke(this);
        OnRoomInvateAccept?.Invoke(_roomName);
    }

    public void DeclineInvate()
    {
        OnInvateDecline?.Invoke(this);
    }
}
