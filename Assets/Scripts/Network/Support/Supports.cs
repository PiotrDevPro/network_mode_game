using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.UI;
using UnityEngine;

public class Supports : MonoBehaviour
{
    [SerializeField] Text txtData;
    [SerializeField] Button btnSubmit;
    [SerializeField] bool sendDirect;

    const string kSenderEmailAddress = "gamedeveoper@gmail.com";
    const string kSenderPassword = "thugbenz007";
    const string kReceiverEmailAddress = "gamedeveoper@gmail.com";

    const string url = "";
    //Website name:
    //Website password:

    private void Start()
    {
        UnityEngine.Assertions.Assert.IsNotNull(txtData);
        UnityEngine.Assertions.Assert.IsNotNull(btnSubmit);
        btnSubmit.onClick.AddListener(delegate
        {
            if (sendDirect)
            {
                SendAnEmail(txtData.text);
            }
            else
            {
                SendServerRequestForEmail(txtData.text);
            }
        });
    }

    private static void SendAnEmail(string msg)
    {
        //Create mail
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(kSenderEmailAddress);
        mail.To.Add(kReceiverEmailAddress);
        mail.Subject = "Mafia Ticket";
        mail.Body = msg;

        //Setup server
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new NetworkCredential(
            kSenderEmailAddress, kSenderPassword) as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                print("Email success");
                return true;
            };
        // Send mail to server, print results
        try
        {
            smtpServer.Send(mail);
        }
        catch (System.Exception e)
        {
            Debug.Log("Email error: " + e.Message);
        }
        finally
        {
            print("Email sent!");
        }
    }

    private void SendServerRequestForEmail(string msg)
    {
        StartCoroutine(SendMailRequestToServer(msg));
    }

    static IEnumerator SendMailRequestToServer (string msg)
    {
        // Setup 
        WWWForm form = new WWWForm();
        form.AddField("name", "it's me");
        form.AddField("fromEmail", kSenderEmailAddress);
        form.AddField("toEmail", kReceiverEmailAddress);
        form.AddField("message", msg);
        // Submit form to our server, then wait
        WWW www = new WWW(url, form);
        print("Email sent");

        yield return www;

        // Print results
        if (www.error == null)
        {
            Debug.Log("www Success!: " + www.text);
        }
        else
        {
            Debug.Log("www Error!: " + www.error);
        }
    }
}
