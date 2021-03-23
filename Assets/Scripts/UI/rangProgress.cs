using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class rangProgress : MonoBehaviour
{
    public Image fillRang;
    public Text progress;

    private void Start()
    {
        
    }

    private void Update()
    {
        float number;
        number  = fillRang.GetComponent<Image>().fillAmount * 2000;
        progress.text = number.ToString() + "/2000";
    }
}
