using System.Collections.Generic;
using UnityEngine;

namespace Zephrax.FNAFGame.Audio
{
    public class SoundManager : MonoBehaviour
    {
        public List<AudioSource> audioSources = new List<AudioSource>();
        [RangeAttribute(0, 20)] public int chance;
        public float interval;
        private float timer;

        // Start is called before the first frame update
        void Start()
        {
            timer = Time.time + interval;
        }

        // Update is called once per frame
        void Update()
        {
            if (timer + interval < Time.time)
            {
                int randomSource = Random.Range(0, audioSources.Count);
                if (Tools.DiceRollGenerator.hasSuccessfulRoll(chance))
                {
                    if (!audioSources[randomSource].isPlaying)
                    {
                        //print("Playing on " + randomSource);
                        audioSources[randomSource].Play();
                    }
                }
                //else { print("audio roll failed");}

                timer = Time.time;
            }
        }


    }
}
