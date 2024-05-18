using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lance
{
    public class AudioManager1 : MonoBehaviour
    {
        [Header("--------------Audio Source---------------")]
        [SerializeField] AudioSource musicSource;
        [SerializeField] AudioSource SFXSource;

        [Header("--------------Audio Clip---------------")]
        public AudioClip background;

        [Header("--------------Fade Settings---------------")]
        public float fadeDuration = 2.0f; // Duration for fade-in and fade-out
        public float mainMenuFadeDuration = 1.0f; // Fade duration specifically for main menu

        private void Start()
        {
            // Set up the audio source with the background clip
            musicSource.clip = background;

            // Start playing the music with fade-in
            StartCoroutine(FadeIn(musicSource, fadeDuration));
        }

        private IEnumerator FadeIn(AudioSource audioSource, float duration)
        {
            float currentTime = 0;
            float startVolume = 0; // Start with zero volume
            audioSource.volume = startVolume;

            audioSource.Play(); // Start playing the music

            // Fade in the volume
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, 1, currentTime / duration);
                yield return null;
            }

            audioSource.volume = 1; // Ensure volume is set to max after fading in
        }

        private IEnumerator FadeOut(AudioSource audioSource, float duration)
        {
            float currentTime = 0;
            float startVolume = audioSource.volume;

            // Fade out the volume
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, 0, currentTime / duration);
                yield return null;
            }

            audioSource.Stop(); // Stop playing the music after fading out
            audioSource.volume = startVolume; // Reset volume for the next play
        }

        private IEnumerator LoopMusic()
        {
            while (true)
            {
                // Wait for the music to nearly finish
                yield return new WaitForSeconds(background.length - fadeDuration);

                // Reset the playback position
                musicSource.time = 0;
            }
        }

        private void Awake()
        {
            // Ensure that the music source is set to loop
            musicSource.loop = true;

            // Don't destroy this object when loading a new scene
            DontDestroyOnLoad(gameObject);

            // Start the coroutine to handle looping
            StartCoroutine(LoopMusic());

            // Subscribe to scene change events
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            // Unsubscribe from scene change events
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Main Menu")
            {
                StopMusic(mainMenuFadeDuration); // Use different fade duration for main menu
            }
            else
            {
                StartMusic();
            }
        }

        private void StopMusic(float duration)
        {
            // Start fading out with the specified duration
            StartCoroutine(FadeOut(musicSource, duration));
        }

        private void StartMusic()
        {
            // Start fading in with the default duration
            StartCoroutine(FadeIn(musicSource, fadeDuration));
        }
    }
}