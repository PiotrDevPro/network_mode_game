using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SendToForm : MonoBehaviour
{
    [SerializeField] InputField feedback1;
    [SerializeField] Button sendBtn;

    string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSeEmabaoiT3qvmQw4PNhYHV1cxInNzLvSF6-oVQg-gTFW_rpw/formResponse";


    public void Send()
    {
        if (feedback1.text == "")
        {
            return;
        }
        StartCoroutine(Post(feedback1.text));
        feedback1.text = "";
        ActiveBtn();
        Invoke("DeactivBtn",1f);
    }

    IEnumerator Post(string s1)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1059067847", s1);




        UnityWebRequest www = UnityWebRequest.Post(URL, form);

        yield return www.SendWebRequest();

    }

    void ActiveBtn()
    {
        sendBtn.interactable = false;
        if (PlayerPrefs.GetInt("lang") == 0)
        {
            sendBtn.GetComponentInChildren<Text>().text = "Отправлено";
        } 
        if (PlayerPrefs.GetInt("lang") == 1)
        {
            sendBtn.GetComponentInChildren<Text>().text = "Done";
        }
        
    }

    void DeactivBtn()
    {
        sendBtn.interactable = true;
        if (PlayerPrefs.GetInt("lang") == 0)
        {
            sendBtn.GetComponentInChildren<Text>().text = "Отправить сообщение";
        }
        if (PlayerPrefs.GetInt("lang") == 1)
        {
            sendBtn.GetComponentInChildren<Text>().text = "Send";
        }
    }
}
