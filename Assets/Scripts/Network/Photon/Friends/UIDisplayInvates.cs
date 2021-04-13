using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisplayInvates : MonoBehaviour
{
    public static UIDisplayInvates manage;
    [SerializeField] private Transform inviteContainer;
    [SerializeField] private UIInvate uiInvatePrefab;
    [SerializeField] private RectTransform contentRect;
    [SerializeField] private Vector2 originalSize;
    [SerializeField] private Vector2 increaseSize;

    private List<UIInvate> invates;

    private void Awake()
    {
        manage = this;
    }

    public void UIDisplayFriendsInvatesOnEnter()
    {
        invates = new List<UIInvate>();
        //contentRect = inviteContainer.GetComponent<RectTransform>();
        //originalSize = contentRect.sizeDelta;
        //increaseSize = new Vector2(0, uiInvatePrefab.GetComponent<RectTransform>().sizeDelta.y);
        ChatController.OnRoomInvite += HandleRoomInvate;
        UIInvate.OnInvateAccept += HandleInviteAccept;
        UIInvate.OnInvateDecline += HandleInviteDecline;
    }

    private void HandleInviteDecline(UIInvate invite)
    {
        if (invates.Contains(invite))
        {
            invates.Remove(invite);
            Destroy(invite.gameObject);
        }
    }

    private void HandleInviteAccept(UIInvate invite)
    {
        if (invates.Contains(invite))
        {
            invates.Remove(invite);
            Destroy(invite.gameObject);
        }
    }

    public void UIDisplayFriendsInvatesOnExit()
    {
        ChatController.OnRoomInvite -= HandleRoomInvate;
        UIInvate.OnInvateAccept -= HandleInviteAccept;
        UIInvate.OnInvateDecline -= HandleInviteDecline;

    }

    private void HandleRoomInvate(string friend, string room)
    {
        print("Room invate for" + friend + "in room" + room);
        UIInvate uiInvate = Instantiate(uiInvatePrefab, inviteContainer);
        uiInvate.Initialize(friend,room);
        //contentRect.sizeDelta += increaseSize;
        invates.Add(uiInvate);
    }
}
