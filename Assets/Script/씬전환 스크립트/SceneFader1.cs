using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader1 : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;
    [HideInInspector]
    public string sceneToLoad;

    private void Start()
    {
        // 처음 씬 로드 시 페이드 인 효과
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        sceneToLoad = sceneName;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(0f, 0f, 0f, 1f - timer / fadeDuration);
            yield return null;
        }
        fadeImage.color = Color.clear;
    }

    private IEnumerator FadeOut()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(0f, 0f, 0f, timer / fadeDuration);
            yield return null;
        }
        fadeImage.color = Color.black;
        SceneManager.LoadScene(sceneToLoad);
    }
}
