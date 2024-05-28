using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Zephrax.FNAFGame.Settings
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private Slider masterSlider, musicSlider, sfxSlider, interfaceSlider;
        [SerializeField] private TMP_Text masterText, musicText, sfxText, interfaceText;
        [SerializeField] private bool hasInterface;
        private float master, music, sfx, interfac;
        private bool settingsLoaded;

        // Start is called before the first frame update
        void Start()
        {
            settingsLoaded = false;

            LoadAudioSettings();
        }

        public void LoadAudioSettings()
        {
            master = PlayerPrefs.GetFloat("audioMasterVolume");

            music = PlayerPrefs.GetFloat("audioMusicVolume");

            sfx = PlayerPrefs.GetFloat("audioSfxVolume");

            interfac = PlayerPrefs.GetFloat("audioInterfaceVolume");

            if (hasInterface)
            {
                UpdateInterfaceValues();
            }

            settingsLoaded = true;
        }

        public void UpdateInterfaceValues()
        {
            masterSlider.value = master;
            masterText.text = master.ToString();

            musicSlider.value = music;
            musicText.text = music.ToString();

            sfxSlider.value = sfx;
            sfxText.text = sfx.ToString();

            interfaceSlider.value = sfx;
            interfaceText.text = sfx.ToString();
        }

        public void SetMasterVolume()
        {
            if (settingsLoaded)
            {
                master = Mathf.Round(masterSlider.value * 100.0f) * 0.01f;
                masterText.text = master.ToString();
                Singleton.Instance.masterVolume = master;
            }
        }
        public void SetMusicVolume()
        {
            if (settingsLoaded)
            {
                music = Mathf.Round(musicSlider.value * 100.0f) * 0.01f;
                musicText.text = music.ToString();
                Singleton.Instance.musicVolume = music;
            }
        }

        public void SetSfxVolume()
        {
            if (settingsLoaded)
            {
                sfx = Mathf.Round(sfxSlider.value * 100.0f) * 0.01f;
                sfxText.text = sfx.ToString();
                Singleton.Instance.sfxVolume = sfx;
            }
        }
        public void SetInterfaceVolume()
        {
            if (settingsLoaded)
            {
                interfac = Mathf.Round(interfaceSlider.value * 100.0f) * 0.01f;
                interfaceText.text = interfac.ToString();
                Singleton.Instance.interfaceVolume = interfac;
            }
        }

        public void SaveAudioSettings()
        {
            PlayerPrefs.SetFloat("audioMasterVolume", master);
            PlayerPrefs.SetFloat("audioMusicVolume", music);
            PlayerPrefs.SetFloat("audioSfxVolume", sfx);
            PlayerPrefs.SetFloat("audioInterfaceVolume", interfac);
            PlayerPrefs.Save();
            print("Saved audio settings to save.");
        }

        public void Cancel()
        {
            master = PlayerPrefs.GetFloat("audioMasterVolume");
            Singleton.Instance.masterVolume = master;
            music = PlayerPrefs.GetFloat("audioMusicVolume");
            Singleton.Instance.musicVolume = music;
            sfx = PlayerPrefs.GetFloat("audioSfxVolume");
            Singleton.Instance.sfxVolume = sfx;
            interfac = PlayerPrefs.GetFloat("audioInterfaceVolume");
            Singleton.Instance.interfaceVolume = interfac;
        }

        public void ToggleMenu(bool setActive)
        {
            if (setActive)
            {
                CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
                canvasGroup.alpha = 1;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
            }
            else
            {
                CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.interactable = false;
            }
        }
    }
}