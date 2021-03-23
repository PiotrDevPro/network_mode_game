using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class loadingScene : MonoBehaviour
{
    [Header("Загружаемая сцена")]
    public int sceneID;
    [Header("Обьекты")]
    public Image loadingImg;

    private void Start()
    {
        StartCoroutine(AsyncLoad());
    }

    IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            loadingImg.fillAmount = progress;
            yield return null;

        }
        
    }
}
