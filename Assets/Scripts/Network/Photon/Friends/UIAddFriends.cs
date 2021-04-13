using System;
using UnityEngine;
using UnityEngine.UI;

public class UIAddFriends : MonoBehaviour
{
    [SerializeField] private string displayName;
    [SerializeField] private TMPro.TextMeshProUGUI friendName;
    public static Action<string> OnAddFriend = delegate { };

    public void SetAddFriendName(string name)
    {

        displayName = name;
        
    }

    public void AddFriend()
    {

        if (string.IsNullOrEmpty(displayName)) return;
        OnAddFriend?.Invoke(displayName);

        //print(string.IsNullOrEmpty(displayName));
    }

}
