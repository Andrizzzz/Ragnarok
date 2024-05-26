using UnityEngine;
using System.Collections;
using FirstGearGames.SmoothCameraShaker;

namespace Lance
{
    public class IsTrigger : MonoBehaviour
    {
        public CameraShaker cameraShaker; // Reference to the CameraShaker component
        public ShakeData earthquakeShakeData; // Shake data for the earthquake effect
        public AudioClip audioClip; // Audio clip to play when triggered.

        private AudioSource audioSource; // Reference to the AudioSource component.

        // Duration of the shake effect.
        public float shakeDuration = 1.0f;

        void Start()
        {
            if (cameraShaker == null)
            {
                Debug.LogError("CameraShaker reference is missing.");
            }
            if (earthquakeShakeData == null)
            {
                Debug.LogError("ShakeData reference is missing.");
            }

            // Add an AudioSource component if not already present.
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            // Assign the audio clip to the AudioSource.
            audioSource.clip = audioClip;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) // Replace "Player" with the tag of the object that triggers the earthquake
            {
                Debug.Log("Trigger detected with Player.");
                cameraShaker.Shake(earthquakeShakeData);

                // Check if audio clip is assigned and play it.
                if (audioClip != null)
                {
                    audioSource.Play();
                    StartCoroutine(FadeOutAudioOverShakeDuration(shakeDuration));
                }
            }
            else
            {
                Debug.Log("Trigger detected with non-Player: " + other.tag);
            }
        }

        private IEnumerator FadeOutAudioOverShakeDuration(float duration)
        {
            float startVolume = audioSource.volume;

            // Gradually decrease the volume over the duration of the shake effect.
            for (float t = 0; t < 1; t += Time.deltaTime / duration)
            {
                audioSource.volume = Mathf.Lerp(startVolume, 0, t);
                yield return null;
            }

            // Ensure the volume is set to zero at the end.
            audioSource.volume = 0;
        }
    }
}
