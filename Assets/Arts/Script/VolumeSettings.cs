using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Lance
{
    public class VolumeSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer myMixer;
        [SerializeField] private Slider musicSlider;

        private void Start()
        {
            if (PlayerPrefs.HasKey("musicVolume"))
            {
                LoadVolume();
            }
            else
            {
                SetMusicVolume();
            }

            // Add listener to save volume whenever the slider value changes
            musicSlider.onValueChanged.AddListener(delegate { SaveVolume(); });
        }

        public void SetMusicVolume()
        {
            float volume = musicSlider.value;
            myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        }

        private void LoadVolume()
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            SetMusicVolume();
        }

        private void SaveVolume()
        {
            float volume = musicSlider.value;
            PlayerPrefs.SetFloat("musicVolume", volume);
            PlayerPrefs.Save();
            SetMusicVolume();
        }
    }
}
