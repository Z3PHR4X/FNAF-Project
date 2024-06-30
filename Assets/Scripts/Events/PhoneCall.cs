using UnityEngine;

namespace Zephrax.FNAFGame.Events
{
    public class PhoneCall : MonoBehaviour
    {
        private float voiceVolume, sfxVolume;
        [SerializeField] private AudioSource phoneAudioSource, startAudioSource, hangupAudioSource;
        [SerializeField] private AudioClip[] phoneClips;
        [SerializeField] private AudioClip startAudioClip, hangupAudioClip;
        [SerializeField] private GameObject phoneUI;
        private bool audioEnded;

        // Start is called before the first frame update
        void Start()
        {
            voiceVolume = (Singleton.Instance.voiceVolume * Singleton.Instance.masterVolume);
            sfxVolume = (Singleton.Instance.sfxVolume * Singleton.Instance.masterVolume);
            phoneAudioSource.volume = voiceVolume;
            startAudioSource.volume = sfxVolume;
            hangupAudioSource.volume = sfxVolume;
            startAudioSource.clip = startAudioClip;
            hangupAudioSource.clip = hangupAudioClip;
            
            int night = Singleton.Instance.selectedNight;

            if (night <= phoneClips.Length)
            {
                startAudioSource.Play();
                phoneAudioSource.clip = phoneClips[night - 1];
                phoneAudioSource.Play();
                audioEnded = false;
                Debug.Log($"playing phone audio: {phoneAudioSource.clip.name}");
                phoneUI.SetActive(Singleton.Instance.canRetryNight);
            }
            else
            {
                phoneUI.SetActive(false);
                audioEnded = true;
            }
        }

        private void Update()
        {
            if (!audioEnded)
            {
                if (Application.isEditor && Input.GetKey(KeyCode.Backspace))
                {
                    HangUpPhone();
                }

                if (!phoneAudioSource.isPlaying)
                {
                    HangUpPhone();
                }
            }
        }
        public void HangUpPhone()
        {
            phoneAudioSource.Stop();
            hangupAudioSource.Play();
            phoneUI.SetActive(false);
            audioEnded = true;
        }
    }


}
