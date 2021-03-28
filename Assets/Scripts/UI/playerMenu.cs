using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class playerMenu : MonoBehaviour
{
    [SerializeField]
    private Button Open;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Button Quit;


    public void openPlayerMenu()
    {
        panel.SetActive(true);
    }

    public void closePlayerPanel()
    {
        panel.SetActive(false);
    }


}
