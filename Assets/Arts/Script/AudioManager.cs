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
        }

        void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Main Menu")
            {
                if (musicClips.Length > 0 && activeSource != null)
                {
                    int randomIndex;
                    do
                    {
                        randomIndex = Random.Range(0, musicClips.Length);
                    }
                    while (musicClips[randomIndex] == activeSource.clip); // Ensure the selected clip is not the same as the currently playing clip

                    activeSource.clip = musicClips[randomIndex];
                    activeSource.Play();
                }
            }
            else
            {
                StartCoroutine(FadeOutMusicAndStop(sceneTransitionFadeDuration));
            }
        }



        IEnumerator PlayNextTrack()
        {
            while (true)
            {
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