using System.Collections.Generic;
using UnityEngine;
using Eflatun.SceneReference;

namespace Zephrax.FNAFGame
{
    public class Level : MonoBehaviour
    {
        public SceneReference levelScene;
        public string levelName;
        [TextAreaAttribute]
        public string levelDescription;
        public Sprite levelIcon, levelThumbnail, controlScheme;
        public Sprite[] levelLoadingBackground;
        public AudioClip selectionSound, selectionMusic;
        public List<Character> characters = new List<Character>();
        public int numberOfNights = 7;
        public int nightLength = 6;
        public float hourLength = 89f;
        public bool supportsCustomNight = true;
    }
}