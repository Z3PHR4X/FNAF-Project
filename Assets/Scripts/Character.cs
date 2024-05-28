using System.Collections.Generic;
using UnityEngine;
using Zephrax.FNAFGame.AI;

namespace Zephrax.FNAFGame
{
    public class Character : MonoBehaviour
    {
        public GameObject ingamePrefab;
        public Sprite thumbnail;
        public string characterName;
        [TextAreaAttribute]
        public string description;
        public List<AIValues> aggressionProgression;
        public float actionInterval;
        public AudioClip soundEffect;
        [RangeAttribute(-3, 3)] public float soundEffectPitch = 1;
    }
}