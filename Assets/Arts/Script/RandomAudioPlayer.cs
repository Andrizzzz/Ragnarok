using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class RandomAudioPlayer : MonoBehaviour
    {
        public AudioClip[] audioClips; // Array to hold audio clips
        private AudioSource audioSource; // Reference to AudioSource component

        void Start()
        {
            audioSource = GetComponent<AudioSource>(); // Get the AudioSource component attached to this GameObject

            if (audioClips.Length > 0) // Check if there are any audio clips in the array
            {
                PlayRandomAudio(); // Play random audio clip when the game starts
            }
            else
            {
                Debug.LogError("No audio clips assigned to play!"); // Log an error if no audio clips are assigned
            }
        }

        void PlayRandomAudio()
        {
            int randomIndex = Random.Range(0, audioClips.Length); // Get a random index within the range of the array
            AudioClip randomClip = audioClips[randomIndex]; // Get the random audio clip from the array
            audioSource.clip = randomClip; // Set the audio clip to be played

            audioSource.Play(); // Play the audio clip
        }
    }
}