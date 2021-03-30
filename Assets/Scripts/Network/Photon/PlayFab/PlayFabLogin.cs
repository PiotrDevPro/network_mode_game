using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayFabLogin : MonoBehaviour
{
    public static PlayFabLogin manage;
    public string username;

    private void Awake()
    {
        manage = this;
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "5225C";
        }
    }

    public void SetUserName(string name)
    {
        if (PlayerPrefs.GetInt("FirstActive") == 0)
        {
            PlayerPrefs.GetString("Nickname");
        }
        else
        {
            username = name;
            PlayerPrefs.SetString("Nickname", username);
        }
        
    }
    public void Login()
    {
        if (!IsValidUserName()) return;

        LoginWithCustomId();
    }

    private bool IsValidUserName()
    {
        bool isValid = false;

        if (username.Length >= 3 && username.Length <= 24)
            isValid = true;
        if (PlayerPrefs.GetString("Nickname") != "")
            isValid = true;

        return isValid;
    }

    private void LoginWithCustomId()
    {
        print("Login to Playfab as " + username);
        var request = new LoginWithCustomIDRequest { CustomId = username, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginCustomIdSuccess, OnFailure);
    }

    private void UpdateDisplayName(string displayname)
    {
        print("Display Name" + displayname);
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = displayname };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request,OnDisplayNameSuccess, OnFailure);
    }

    private void OnDisplayNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        print("You have update Displayname!");
    }

    private void OnLoginCustomIdSuccess(LoginResult obj)
    {
        print("You have logged into Playfab using custom id" + PlayerPrefs.GetString("Nickname"));
        UpdateDisplayName(PlayerPrefs.GetString("Nickname"));
    }

    private void OnFailure(PlayFabError error)
    {
        print("error" + error.GenerateErrorReport());
    }

}
