using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    public bool isDeprecated = true;
    public GameManager gameManager;
    [SerializeField] private GameObject Freddy;
    [SerializeField] private AudioSource musicBox;
    [SerializeField] private GameObject head;

    private bool _hasMusicBoxPlayed;
    private float _musicBoxTimer;
    private int _musicBoxTries;
    private Collider[] cameraBuffer = new Collider[1];
    private int maskId;

    // Start is called before the first frame update
    void Start()
    {
        print("Music Box sequence initiated.");
        gameManager = FindObjectOfType<GameManager>();
        maskId = LayerMask.GetMask("PostProcessing");
        Freddy.SetActive(false);
        _hasMusicBoxPlayed = false;
        _musicBoxTries = 0;
        _musicBoxTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        MusicBoxSequence();
    }

    void LookAtCamera()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, 10f, cameraBuffer, maskId);
        if (count > 0)
        {
            head.transform.LookAt(cameraBuffer[0].transform.position, Vector3.left);
        }
        else
        {
            head.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
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
                    gameManager.isPlayerAlive = false;
                    Time.timeScale = 0f;
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