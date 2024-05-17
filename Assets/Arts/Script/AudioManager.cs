using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Add this namespace

namespace Lance
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource audioSource1;
        public AudioSource audioSource2;
        public AudioClip[] musicClips;
        public float fadeDuration = 1.0f;
        public float sceneTransitionFadeDuration = 0.5f;  // New field for scene transition fade duration

        private int currentClipIndex = 0;
        private bool isFading = false;
        private AudioSource activeSource;
        private AudioSource inactiveSource;

        private static AudioManager instance;
        private bool initialSceneLoaded = false;

        void Awake()
        {
            // Ensure only one instance of AudioManager exists
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;  // Subscribe to sceneLoaded event

            if (musicClips.Length > 0)
            {
                audioSource1.clip = musicClips[0];
                activeSource = audioSource1;
                inactiveSource = audioSource2;

                // Start with zero volume and play the clip
                activeSource.volume = 0;
                activeSource.Play();

                // Gradually increase the volume of the active source to achieve fade-in effect
                StartCoroutine(FadeInMusic(activeSource, fadeDuration));

                // Start coroutine to play the next track
                StartCoroutine(PlayNextTrack());
            }
        }

        IEnumerator FadeInMusic(AudioSource source, float duration)
        {
            float startVolume = 0;
            float endVolume = 1;

            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                source.volume = Mathf.Lerp(startVolume, endVolume, t / duration);
                yield return null;
            }

            source.volume = endVolume;
        }



        void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;  // Unsubscribe from sceneLoaded event
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Avoid fading out on initial scene load
            if (initialSceneLoaded)
            {
                StartCoroutine(FadeOutMusicAndStop(sceneTransitionFadeDuration));  // Use scene transition fade duration
            }
            else
            {
                initialSceneLoaded = true;
            }
        }

        IEnumerator PlayNextTrack()
        {
            while (true)
            {
                // Wait until the remaining time of the current clip is less than the fade duration
                yield return new WaitForSeconds(Mathf.Max(activeSource.clip.length - fadeDuration, 0));

                if (!isFading)
                {
                    StartCoroutine(FadeOutIn());
                }
            }
        }


        IEnumerator FadeOutIn()
        {
            isFading = true;

            currentClipIndex = (currentClipIndex + 1) % musicClips.Length;
            inactiveSource.clip = musicClips[currentClipIndex];
            inactiveSource.Play();

            float startVolume = activeSource.volume;

            // Fade out the active source and fade in the inactive source
            for (float t = 0; t <= fadeDuration; t += Time.deltaTime)
            {
                float normalizedTime = t / fadeDuration;
                activeSource.volume = Mathf.Lerp(startVolume, 0, normalizedTime);
                inactiveSource.volume = Mathf.Lerp(0, startVolume, normalizedTime);
                yield return null;
            }

            activeSource.Stop();
            activeSource.volume = startVolume;

            // Swap active and inactive sources
            AudioSource temp = activeSource;
            activeSource = inactiveSource;
            inactiveSource = temp;

            isFading = false;
        }

        public IEnumerator FadeOutMusicAndStop(float duration)
        {
            float startVolume = activeSource.volume;

            for (float t = 0; t <= duration; t += Time.deltaTime)
            {
                activeSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
                yield return null;
            }

            activeSource.Stop();
            activeSource.volume = startVolume;
        }
    }
}
