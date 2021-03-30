using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class playerMenu : MonoBehaviour
{
    public static playerMenu manage;
    [SerializeField]
    private Button Open;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Button Quit;



    private void Awake()
    {
        manage = this;
    }

    public void openPlayerMenu()
    {
        panel.SetActive(true);
    }

    public void closePlayerPanel()
    {
        panel.SetActive(false);
    }


}
