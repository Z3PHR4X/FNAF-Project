using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Events
{
    public class MusicBox : MonoBehaviour
    {
        [SerializeField] private GameObject Freddy;
        [SerializeField] private AudioSource musicBox;
        [SerializeField] private VideoPlayer jumpscareVideo;

        private bool _hasMusicBoxPlayed;
        private float _musicBoxTimer;
        private int _musicBoxTries;

        // Start is called before the first frame update
        void Start()
        {
            print("Music Box sequence initiated.");
            Freddy.SetActive(false);
            _hasMusicBoxPlayed = false;
            _musicBoxTries = 0;
            _musicBoxTimer = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            if (!GameManagerV2.Instance.hasPlayerWon)
            {
                MusicBoxSequence();
            }
        }

        public virtual IEnumerator Jumpscare()
        {
            jumpscareVideo.targetCamera = Camera.main;
            jumpscareVideo.Play();
            yield return new WaitForSeconds((float)jumpscareVideo.clip.length);
            Player.Instance.isAlive = false;
            yield return new WaitForEndOfFrame();
        }


        void MusicBoxSequence()
        {
            //When he stops singing, he has a 20% chance to jumpscare you every 2 seconds. 
            if (_hasMusicBoxPlayed)
            {
                if (_musicBoxTimer + 2 < Time.time)
                {
                    if (Random.Range(0, 5) == 0)
                    {
                        Singleton.Instance.SetDeathMessage("You ran out of power! Tom Nook will come claim your mortgage payment when it's dark..");
                        StartCoroutine(Jumpscare());
                        //Time.timeScale = 0f;
                    }
                    _musicBoxTimer = Time.time;
                }
            }
            else
            {
                //He also has 20% chance every 5 seconds to stop singing,
                //the maximum time singing being 20 seconds
                if (musicBox.isPlaying)
                {
                    if (_musicBoxTimer + 5 < Time.time)
                    {
                        if (_musicBoxTries > 3 || Random.Range(0, 5) == 0)
                        {
                            musicBox.Stop();
                            Freddy.SetActive(false);
                            _hasMusicBoxPlayed = true;
                            _musicBoxTries = 0;
                        }
                        else
                        {
                            _musicBoxTries++;
                        }
                        _musicBoxTimer = Time.time;
                    }
                }
                else
                {
                    //When the power cuts Freddy has a 20% chance every 5 seconds to start singing,
                    //and 20 seconds being the max amount of time he can go on without singing.
                    if (_musicBoxTimer + 5 < Time.time)
                    {
                        if (_musicBoxTries > 3 || Random.Range(0, 5) == 0)
                        {
                            musicBox.Play();
                            Freddy.SetActive(true);
                            _musicBoxTries = 0;
                        }
                        else
                        {
                            _musicBoxTries++;
                        }
                        _musicBoxTimer = Time.time;
                    }
                }
            }
        }
    }
}