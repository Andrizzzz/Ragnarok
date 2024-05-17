using UnityEngine;
using UnityEngine.EventSystems;

namespace Lance
{
    public class ButtonSFX : MonoBehaviour, IPointerClickHandler
    {
        public AudioClip clickSound;
        private AudioSource audioSource;

        void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.enabled = true; 
            audioSource.volume = 1f; 
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (clickSound != null)
            {
                
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(clickSound);
                }
            }
        }
    }
}
