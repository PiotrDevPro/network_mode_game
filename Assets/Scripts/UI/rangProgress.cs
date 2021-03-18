using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class rangProgress : MonoBehaviour
{
    public Image fillRang;
    public Text progress;

    private void Update()
    {
        float number;
        number  = fillRang.fillAmount * 100;
        progress.text = number.ToString() + "/100";
    }
}
