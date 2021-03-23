using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Менюшки")]
    public GameObject _play;
    public GameObject _options;
    public GameObject _friends;
    public GameObject _mainmenu;
    public GameObject _rules;
    public GameObject _scrollPanel;
    [Header("Настройка")]
    public GameObject _support;
    public Toggle musicOnOff;

    void Awake()
    {
        musicOnOff.isOn = (PlayerPrefs.GetInt("Sound") == 0) ? true : false;
    }

    void Start()
    {
        //AudioListener.pause = (PlayerPrefs.GetInt("Sound") == 1) ? true : false;
    }

    void Update()
    {
        //PlayerPrefs.DeleteAll();
    }

    #region MainMenu
    public void PlayPanel()
    {
        _play.SetActive(true);
    }
    public void PlayPanelQuit()
    {
        _play.SetActive(false);
    }
    public void Options()
    {
        _options.SetActive(true);
    }
    public void OptionsQuit()
    {
        _options.SetActive(false);
    }

    public void Friends()
    {
        _friends.SetActive(true);
        _scrollPanel.GetComponentInChildren<ScrollRect>().enabled = false;
    }

    public void MainMenu()
    {
        _friends.SetActive(false);
        _mainmenu.SetActive(true);
        _scrollPanel.GetComponentInChildren<ScrollRect>().enabled = true;
    }

    public void Rules()
    {
        _rules.SetActive(true);
    }

    public void RulesExit()
    {
        _rules.SetActive(false);
    }

    #endregion

    #region Settings

    public void Support()
    {
        _support.SetActive(true);
        _options.SetActive(false);
    }

    public void SupportQuit()
    {
        _support.SetActive(false);
        _options.SetActive(true);
    }

    public void policy()
    {
        Application.OpenURL("http://google.com");
    }

    public void musiconoff(Toggle tgl)
    {
        if (tgl.isOn)
        {
            PlayerPrefs.SetInt("Sound",0);
            AudioListener.pause = false;
        } 
        else
        {
            PlayerPrefs.SetInt("Sound",1);
            AudioListener.pause = true;
        }
    }

    #endregion

}
