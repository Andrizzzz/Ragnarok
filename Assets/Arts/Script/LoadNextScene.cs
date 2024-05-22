using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Lance
{
    public class LoadNextScene : MonoBehaviour
    {
        public float fadeDuration = 1.0f;

        private Image fadeOverlay;
        private Button button;

        void Start()
        {
            
            CreateFadeOverlay();

            
            button = GetComponent<Button>();

            
            Debug.Log("Button reference: " + button);

            
            if (button == null)
            {
                Debug.LogWarning("Button component not found on GameObject!");
            }
        }

        public void LoadSceneWithFade()
        {
            StartCoroutine(FadeAndLoadScene());
        }

        IEnumerator FadeAndLoadScene()
        {
            if (fadeOverlay == null)
            {
                Debug.LogError("Fade overlay not found!");
                yield break;
            }

            
            if (button != null)
            {
                Debug.Log("Disabling button...");
                button.interactable = false;
                Debug.Log("Button interactable: " + button.interactable);
            }

            
            float startTime = Time.time;
            while (Time.time - startTime < fadeDuration)
            {
                float normalizedTime = (Time.time - startTime) / fadeDuration;
                fadeOverlay.color = new Color(0, 0, 0, normalizedTime);
                yield return null;
            }

            
            SceneManager.LoadScene("Cutscene");
        }

        private void CreateFadeOverlay()
        {
            
            GameObject overlayGO = new GameObject("FadeOverlay");
            overlayGO.transform.SetParent(transform, false);

            
            fadeOverlay = overlayGO.AddComponent<Image>();
            fadeOverlay.rectTransform.anchorMin = Vector2.zero;
            fadeOverlay.rectTransform.anchorMax = Vector2.one;
            fadeOverlay.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            fadeOverlay.rectTransform.anchoredPosition = Vector2.zero;
            fadeOverlay.rectTransform.sizeDelta = Vector2.zero;
            fadeOverlay.color = Color.clear; 
            fadeOverlay.raycastTarget = false; 
        }
    }
}
