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
            // Create the fade overlay dynamically
            CreateFadeOverlay();

            // Get the Button component attached to this GameObject
            button = GetComponent<Button>();

            // Log the button reference
            Debug.Log("Button reference: " + button);

            // Check if Button component is attached
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

            // Disable the button during the fade effect
            if (button != null)
            {
                Debug.Log("Disabling button...");
                button.interactable = false;
                Debug.Log("Button interactable: " + button.interactable);
            }

            // Fade in
            float startTime = Time.time;
            while (Time.time - startTime < fadeDuration)
            {
                float normalizedTime = (Time.time - startTime) / fadeDuration;
                fadeOverlay.color = new Color(0, 0, 0, normalizedTime);
                yield return null;
            }

            // Load the scene
            SceneManager.LoadScene("Level1-1");
        }

        private void CreateFadeOverlay()
        {
            // Create a new GameObject for the fade overlay
            GameObject overlayGO = new GameObject("FadeOverlay");
            overlayGO.transform.SetParent(transform, false);

            // Add an Image component to the fade overlay GameObject
            fadeOverlay = overlayGO.AddComponent<Image>();
            fadeOverlay.rectTransform.anchorMin = Vector2.zero;
            fadeOverlay.rectTransform.anchorMax = Vector2.one;
            fadeOverlay.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            fadeOverlay.rectTransform.anchoredPosition = Vector2.zero;
            fadeOverlay.rectTransform.sizeDelta = Vector2.zero;
            fadeOverlay.color = Color.clear; // Start with transparent color
            fadeOverlay.raycastTarget = false; // Ensure it doesn't block raycasts
        }
    }
}
