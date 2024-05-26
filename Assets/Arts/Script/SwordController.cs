using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class SwordController : MonoBehaviour
    {
        public static SwordController instance; // Singleton instance

        private AudioSource audioSource;

        public AudioClip swordSwingSFX1;
        public AudioClip swordSwingSFX2;
        public AudioClip swordSwingSFX3;
        public AudioClip jumpSFX;
        public AudioClip dashSFX;
        public AudioClip landingSFX;
        public AudioClip grabSFX;
        public AudioClip runningSFX;


        void Start()
        {
            instance = this; // Set the singleton instance
            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySwordSwingSound1()
        {
            StartCoroutine(PlaySoundCoroutine(swordSwingSFX1));
        }

        public void PlaySwordSwingSound2()
        {
            StartCoroutine(PlaySoundCoroutine(swordSwingSFX2));
        }

        public void PlaySwordSwingSound3()
        {
            StartCoroutine(PlaySoundCoroutine(swordSwingSFX3));
        }

        public void PlayJumpSound()
        {
            StartCoroutine(PlaySoundCoroutine(jumpSFX));
        }

        public void PlayDashSound()
        {
            StartCoroutine(PlaySoundCoroutine(dashSFX));
        }

        public void PlayLandingSound()
        {
            StartCoroutine(PlaySoundCoroutine(landingSFX));
        }

        public void PlayGrabSound()
        {
            StartCoroutine(PlaySoundCoroutine(grabSFX));
        }

        public void PlayRunningSound()
        {
            StartCoroutine(PlaySoundCoroutine(runningSFX));
        }

        private IEnumerator PlaySoundCoroutine(AudioClip clip)
        {
            if (audioSource != null && clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
                // Wait until the sound finishes playing
                yield return new WaitForSeconds(clip.length);
            }
            else
            {
                Debug.LogWarning("AudioSource or AudioClip not found on the GameObject.");
            }
        }
    }
}
