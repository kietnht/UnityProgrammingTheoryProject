using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] CanvasGroup fade;
    [SerializeField] float fadeTime = 2;
    public void StartGame()
    {
        StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        fade.alpha = 0.0f;
        float time = 0;
        while(time < fadeTime)
        {
            fade.alpha = Mathf.Min(1, (time + 0.5f) / fadeTime);
            time += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(1);
    }

}
