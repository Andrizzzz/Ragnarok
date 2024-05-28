using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lance
{
    public class FinishPoint : MonoBehaviour
    {
        private bool isTransitioning = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !isTransitioning)
            {
                isTransitioning = true;
                LoadingScreenManager.instance.ShowLoadingScreen();
                SceneController.instance.NextLevel();
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (isTransitioning)
            {
                LoadingScreenManager.instance.HideLoadingScreen();
                SceneManager.sceneLoaded -= OnSceneLoaded;
                isTransitioning = false;
            }
        }
    }
}

