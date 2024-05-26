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


        void Start()
        {
            instance = this; // Set the singleton instance

            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySwordSwingSound1()
        {
            PlaySound(swordSwingSFX1);
        }

        public void PlaySwordSwingSound2()
        {
            PlaySound(swordSwingSFX2);
        }

        public void PlaySwordSwingSound3()
        {
            PlaySound(swordSwingSFX3);
        }

        public void PlayJumpSound()
        {
            PlaySound(jumpSFX);
        }

        public void PlayDashSound()
        {
            PlaySound(dashSFX);
        }

        public void PlayLandingSound()
        {
            PlaySound(landingSFX);
        }

        public void PlayGrabSound()
        {
            PlaySound(grabSFX);
        }

        private void PlaySound(AudioClip clip)
        {
            if (audioSource != null && clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource or AudioClip not found on the GameObject.");
            }
        }
    }
}