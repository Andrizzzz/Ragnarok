using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class EnemySFX : MonoBehaviour
    {
        public static EnemySFX instance; // Singleton instance

        private AudioSource audioSource;

        public AudioClip bowLoadingSFX;
        public AudioClip bowReleaseSFX;
        public AudioClip enemyPunchSFX;
        public AudioClip slimeBiteSFX;
        public AudioClip wormAttackSFX;
        public AudioClip wormAttackSFX2;


        void Start()
        {
            instance = this; // Set the singleton instance
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayBowLoading()
        {
            StartCoroutine(PlaySoundCoroutine(bowLoadingSFX));
        }

        public void PlayBowRelease()
        {
            StartCoroutine(PlaySoundCoroutine(bowReleaseSFX));
        }

        public void PlayEnemyPunch()
        {
            StartCoroutine(PlaySoundCoroutine(enemyPunchSFX));
        }

        public void PlaySlimeBite()
        {
            StartCoroutine(PlaySoundCoroutine(slimeBiteSFX));
        }

        public void PlayWormAttack()
        {
            StartCoroutine(PlaySoundCoroutine(wormAttackSFX));
        }

        public void PlayWormAttack2()
        {
            StartCoroutine(PlaySoundCoroutine(wormAttackSFX2));
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

