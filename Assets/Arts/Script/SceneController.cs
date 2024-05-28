using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lance;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public GameObject loadingScreen;
    public GameObject loadingText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        StartCoroutine(LoadNextLevelAsync());
    }

    private IEnumerator LoadNextLevelAsync()
    {
        LoadingScreenManager loadingScreenManager = LoadingScreenManager.instance;
        if (loadingScreenManager != null)
        {
            loadingScreenManager.ShowLoadingScreen();
            yield return new WaitForSeconds(loadingScreenManager.loadingScreenDuration);
        }
        else
        {
            Debug.LogWarning("LoadingScreenManager not found. Using default duration.");
            yield return new WaitForSeconds(2.0f); // Default duration if LoadingScreenManager is not found
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (loadingScreenManager != null)
        {
            loadingScreenManager.HideLoadingScreen();
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        LoadingScreenManager loadingScreenManager = LoadingScreenManager.instance;
        if (loadingScreenManager != null)
        {
            loadingScreenManager.ShowLoadingScreen();
            yield return new WaitForSeconds(loadingScreenManager.loadingScreenDuration);
        }
        else
        {
            Debug.LogWarning("LoadingScreenManager not found. Using default duration.");
            yield return new WaitForSeconds(2.0f); // Default duration if LoadingScreenManager is not found
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (loadingScreenManager != null)
        {
            loadingScreenManager.HideLoadingScreen();
        }
    }
}
