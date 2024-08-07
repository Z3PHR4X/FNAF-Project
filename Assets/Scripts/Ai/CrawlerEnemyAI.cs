using UnityEngine;

namespace Zephrax.FNAFGame.AI {
    //Crawling AI Variant, can move through vents to attack the player.
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
