using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public enum SceneDirectory
    {
        TitleScene,
        CustomizationScene,
        GameplayScene
    }

    [SerializeField]
    private Image curtain;
    [SerializeField]
    private float transitionDuration = 1f;
    [SerializeField]
    private SceneDirectory nextScene;

    public static SceneLoader instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Called when the game is terminated
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(StartSequenceCoroutine());
    }
    
    private IEnumerator StartSequenceCoroutine()
    {
        curtain.gameObject.SetActive(true);
        yield return UIFadeCoroutine(curtain, 1, 0, transitionDuration);
        curtain.gameObject.SetActive(false);
    }

    public void LoadScene()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    public IEnumerator LoadSceneCoroutine()
    {
        curtain.gameObject.SetActive(true);
        yield return UIFadeCoroutine(curtain, 0, 1, transitionDuration);

        SceneManager.LoadScene(nextScene.ToString());
    }

    private IEnumerator UIFadeCoroutine(Image image, float a, float b, float time)
    {
        float timer = 0f;
        while(timer < time)
        {
            image.color = Color.Lerp(new Color(0, 0, 0, a), new Color(0, 0, 0, b), timer / time);

            timer += Time.deltaTime;

            yield return 0;
        }

        image.color = new Color(0, 0, 0, b);
    }
}
