using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephrax.FNAFGame.Events
{
    public class PhoneCall : MonoBehaviour
    {
        private float voiceVolume;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] phoneClips;

        // Start is called before the first frame update
        void Start()
        {
            voiceVolume = (Singleton.Instance.voiceVolume * Singleton.Instance.masterVolume);
            int night = Singleton.Instance.selectedNight;

            if (night < 6)
            {
                audioSource.clip = phoneClips[night - 1];
                audioSource.volume = voiceVolume;
                audioSource.Play();
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Backspace))
            {
                audioSource.Stop();
            }
        }
    }
}
