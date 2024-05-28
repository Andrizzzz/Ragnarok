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

        private bool audioMuted = false;
        private float previousVolume;

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

        private void Start()
        {
            // Subscribe to scene loaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            // Unsubscribe from scene loaded event
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void ShowLoadingScreen()
        {
            if (loadingScreenCanvas != null)
            {
                loadingScreenCanvas.SetActive(true);
                // Mute audio when loading screen is active
                MuteAudio();
            }
        }

        public void HideLoadingScreen()
        {
            if (loadingScreenCanvas != null)
            {
                loadingScreenCanvas.SetActive(false);
                // Resume audio when loading screen is inactive
                UnmuteAudio();
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

        private void MuteAudio()
        {
            if (!audioMuted)
            {
                previousVolume = AudioListener.volume;
                AudioListener.volume = 0; // Mute audio
                audioMuted = true;
            }
        }

        private void UnmuteAudio()
        {
            if (audioMuted)
            {
                AudioListener.volume = previousVolume; // Restore previous volume
                audioMuted = false;
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // When a scene is loaded, unmute the audio
            if (audioMuted)
            {
                UnmuteAudio();
            }
        }
    }
}
