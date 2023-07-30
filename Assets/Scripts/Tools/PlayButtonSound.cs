using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class PlayButtonSound : MonoBehaviour
    {
        private Audio.MenuSoundPlayer menuSoundPlayer;

        // Start is called before the first frame update
        void Awake()
        {
            menuSoundPlayer = FindObjectOfType<Audio.MenuSoundPlayer>();
        }

        public void PlayConfirmSound()
        {
            menuSoundPlayer.PlayConfirmSound();
        }

        public void PlayCancelSound()
        {
            menuSoundPlayer.PlayCancelSound();
        }

        public void PlayHoverSound()
        {
            menuSoundPlayer.PlayHoverSound();
        }
    }
}
