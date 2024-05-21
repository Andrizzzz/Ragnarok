using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class AudioManagerScenes : MonoBehaviour
    {


        [Header("------------Audio Source ------------")]
        [SerializeField] AudioSource musicSource;
        [SerializeField] AudioSource SFXSource;


        [Header("------------Audio Clip ------------")]
        public AudioClip background;



        private void Start ()
        {
            musicSource.clip = background;
            musicSource.Play();
        }



    }
}