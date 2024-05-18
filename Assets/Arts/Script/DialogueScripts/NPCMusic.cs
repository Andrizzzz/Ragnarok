using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class NPCMusic : MonoBehaviour
    {

        public AudioSource audioSource;
        public AudioClip npcMusic, defaultMusic;



        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            defaultMusic = audioSource.clip;
        }

        private void OnTriggerEnter2D(Collider collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                audioSource.clip = npcMusic;
                audioSource.Play();
            }
        }


        private void OnTriggerExit2D(Collider collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                audioSource.clip = defaultMusic;
                audioSource.Play();
            }
        }
    }
}