using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Lance
{
    public class LoadingScreenManager : MonoBehaviour
    {
        public static LoadingScreenManager instance;
        public GameObject loadingScreenCanvas;
        public float loadingScreenDuration = 2.0f; // Adjust the duration as needed

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

        public void ShowLoadingScreen()
        {
            if (loadingScreenCanvas != null)
            {
                loadingScreenCanvas.SetActive(true);
            }
        }

        public void HideLoadingScreen()
        {
            if (loadingScreenCanvas != null)
            {
                loadingScreenCanvas.SetActive(false);
            }
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneWithLoadingScreen(sceneName));
        }

        private IEnumerator LoadSceneWithLoadingScreen(string sceneName)
        {
            Debug.Log("Loading screen shown");
            ShowLoadingScreen();
            yield return new WaitForSeconds(loadingScreenDuration);
            Debug.Log("Loading screen hidden, starting to load scene");
            SceneManager.LoadScene(sceneName);
        }
    }
}
