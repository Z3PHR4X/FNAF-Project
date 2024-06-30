using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zephrax.FNAFGame.UserInterface.Menus;

namespace Zephrax.FNAFGame.Settings
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private Slider masterSlider, musicSlider, voiceSlider, sfxSlider, interfaceSlider;
        [SerializeField] private TMP_Text masterText, musicText, voiceText, sfxText, interfaceText;
        [Header("MenuMusic Add-on")]
        [SerializeField] private TMP_Dropdown musicSelection;
        [SerializeField] private Toggle musicOverride;
        [SerializeField] private MainMenuMusic mainMenuMusic;
        private int musicSel;

        [Header("Other")]
        [SerializeField] private bool hasInterface;
        private float master, music, voice, sfx, interfac;
        private bool settingsLoaded, musOverride;

        // Start is called before the first frame update
        void Start()
        {
            settingsLoaded = false;
            mainMenuMusic = FindFirstObjectByType<MainMenuMusic>();
            LoadAudioSettings();
        }

        public void LoadAudioSettings()
        {
            master = PlayerPrefs.GetFloat("audioMasterVolume");

            music = PlayerPrefs.GetFloat("audioMusicVolume");

            voice = PlayerPrefs.GetFloat("audioVoiceVolume");

            sfx = PlayerPrefs.GetFloat("audioSfxVolume");

            interfac = PlayerPrefs.GetFloat("audioInterfaceVolume");

            if (Singleton.Instance.completedNight >= 7)
            {
                musOverride = PlayerPrefs.GetInt("menuMusicOverride") == 1;
            }
            else

            {
                musOverride = false;
                PlayerPrefs.SetInt("menuMusicOverride", 0);
            }

            if (hasInterface)
            {
                UpdateInterfaceValues();
            }

            settingsLoaded = true;
        }

        public void UpdateInterfaceValues()
        {
            musicOverride.isOn = musOverride;
            musicOverride.interactable = (Singleton.Instance.completedNight >= 7 && mainMenuMusic != null);

            masterSlider.value = master;
            masterText.text = master.ToString();

            musicSlider.value = music;
            musicText.text = music.ToString();

            voiceSlider.value = voice;
            voiceText.text = voice.ToString();

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

        public void SetVoiceVolume()
        {
            if (settingsLoaded)
            {
                voice = Mathf.Round(voiceSlider.value * 100.0f) * 0.01f;
                voiceText.text = voice.ToString();
                Singleton.Instance.voiceVolume = voice;
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

        public void SetMusicOverride()
        {
            if (musicOverride.isOn)
            {
                PlayerPrefs.SetInt("menuMusicOverride", 1);
                musOverride = true;
                if (musicOverride.interactable)
                {
                    musicSelection.interactable = true;
                }
            }
            else
            {
                PlayerPrefs.SetInt("menuMusicOverride", 0);
                musOverride = false;
                if (musicOverride.interactable)
                {
                    musicSelection.interactable = false;
                }

            }
            if (settingsLoaded && mainMenuMusic != null)
            {
                mainMenuMusic.SetupMusicSetting(musicOverride.isOn, musicSel);
            }
            PlayerPrefs.Save();
        }

        public void SetMusicSelection()
        {
            musicSel = musicSelection.value;
            PlayerPrefs.SetInt("menuMusicSelection", musicSel);
            if (settingsLoaded && mainMenuMusic != null)
            {
                mainMenuMusic.SetupMusicSetting(musicOverride.isOn, musicSel);
            }
            PlayerPrefs.Save();
        }

        public void SaveAudioSettings()
        {
            PlayerPrefs.SetFloat("audioMasterVolume", master);
            PlayerPrefs.SetFloat("audioMusicVolume", music);
            PlayerPrefs.SetFloat("audioVoiceVolume", voice);
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
            voice = PlayerPrefs.GetFloat("audioVoiceVolume");
            Singleton.Instance.voiceVolume = voice;
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