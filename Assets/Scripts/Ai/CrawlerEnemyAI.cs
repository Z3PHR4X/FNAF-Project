using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class CrawlerEnemyAI : DefaultEnemyAI
    {
        public AudioClip crawlAudio;

        public override void PlayAudio(string audioType, bool isRandom)
        {
            switch (audioType)
            {
                case "attack":
                    characterAudioSource.clip = attackAudio;
                    characterAudioSource.Play();
                    break;

                case "attackFail":
                    characterAudioSource.clip = attackFailAudio;
                    characterAudioSource.Play();
                    break;

                case "movement":
                    if (currentWaypoint.isCrawling)
                    {
                        characterAudioSource.clip = crawlAudio;
                    }
                    else
                    {
                        characterAudioSource.clip = movementAudio;
                    }

                    characterAudioSource.Play();
                    break;

                default:

                    if (isRandom)
                    {
                        characterRandomAudioSource.clip = defaultAudio;
                        characterRandomAudioSource.Play();
                    }
                    else
                    {
                        characterAudioSource.clip = defaultAudio;
                        characterAudioSource.Play();
                    }
                    break;
            }
        }
    }
}
