using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SendToForm : MonoBehaviour
{
    [SerializeField] InputField feedback1;

    string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSeEmabaoiT3qvmQw4PNhYHV1cxInNzLvSF6-oVQg-gTFW_rpw/formResponse";


    public void Send()
    {
        StartCoroutine(Post(feedback1.text));
    }

    IEnumerator Post(string s1)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1059067847", s1);




        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();

    }

}
