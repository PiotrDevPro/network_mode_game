using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class lang : MonoBehaviour
{
    [SerializeField] Sprite rusActive;
    [SerializeField] Sprite rusInnactive;
    [SerializeField] Sprite engActive;
    [SerializeField] Sprite engInnactive;
    [SerializeField] Button rus;
    [SerializeField] Button eng;

    private void Start()
    {
        if (PlayerPrefs.GetInt("lang") == 0)
        {
            rus.GetComponent<Image>().sprite = rusActive;
            eng.GetComponent<Image>().sprite = engInnactive;
        }

        if (PlayerPrefs.GetInt("lang") == 1)
        {
            rus.GetComponent<Image>().sprite = rusInnactive;
            eng.GetComponent<Image>().sprite = engActive;
        }
    }

    public void _rus()
    {
        PlayerPrefs.SetInt("lang", 0);
        rus.GetComponent<Image>().sprite = rusActive;
        eng.GetComponent<Image>().sprite = engInnactive;
    }

    public void _eng()
    {
        PlayerPrefs.SetInt("lang",1);
        rus.GetComponent<Image>().sprite = rusInnactive;
        eng.GetComponent<Image>().sprite = engActive;
    }
}
