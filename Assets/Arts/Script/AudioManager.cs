using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lance
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource audioSource1;
        public AudioSource audioSource2;
        public AudioClip[] musicClips;
        public float fadeDuration = 1.0f;
        public float sceneTransitionFadeDuration = 0.5f;

        private int currentClipIndex = 0;
        private bool isFading = false;
        private bool isMusicPlaying = false; // Flag to track if music should be playing
        private AudioSource activeSource;
        private AudioSource inactiveSource;

        private static AudioManager instance;

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;

            // Set initial music clip and start playing
            SetInitialMusicClip();
        }

        void SetInitialMusicClip()
        {
            if (musicClips.Length > 0)
            {
                audioSource1.clip = musicClips[0];
                activeSource = audioSource1;
                inactiveSource = audioSource2;

                activeSource.volume = 0;
                activeSource.Play();

                StartCoroutine(FadeInMusic(activeSource, fadeDuration));

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
            isMusicPlaying = true; // Music is fully faded in and playing
        }

        void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Main Menu")
            {
                StartCoroutine(PlayMusicInMainMenu());
            }
            else
            {
                if (isMusicPlaying && !isFading) // Check if music should be playing and not currently fading
                {
                    StartCoroutine(FadeOutMusicAndStop(sceneTransitionFadeDuration));
                }
            }
        }

        IEnumerator PlayNextTrack()
        {
            while (true)
            {
                yield return new WaitForSeconds(Mathf.Max(activeSource.clip.length - fadeDuration, 0));

                if (!isFading && isMusicPlaying)
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

            for (float t = 0; t <= fadeDuration; t += Time.deltaTime)
            {
                float normalizedTime = t / fadeDuration;
                activeSource.volume = Mathf.Lerp(startVolume, 0, normalizedTime);
                inactiveSource.volume = Mathf.Lerp(0, startVolume, normalizedTime);
                yield return null;
            }

            activeSource.Stop();
            activeSource.volume = startVolume;

            AudioSource temp = activeSource;
            activeSource = inactiveSource;
            inactiveSource = temp;

            isFading = false;
        }

        public IEnumerator FadeOutMusicAndStop(float duration)
        {
            isMusicPlaying = false; // Music should not be playing anymore

            float startVolume = activeSource.volume;

            for (float t = 0; t <= duration; t += Time.deltaTime)
            {
                activeSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
                yield return null;
            }

            activeSource.Stop();
            activeSource.volume = startVolume;
        }

        IEnumerator PlayMusicInMainMenu()
        {
            yield return new WaitForSeconds(0.1f); // Adjust the delay time if needed

            if (!isMusicPlaying)
            {
                // Play a random music clip if not already playing
                if (musicClips.Length > 0)
                {
                    int randomIndex = Random.Range(0, musicClips.Length);
                    activeSource.clip = musicClips[randomIndex];
                    activeSource.Play();
                    StartCoroutine(FadeInMusic(activeSource, fadeDuration));
                    isMusicPlaying = true;
                }
            }
        }
    }
}
