using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public DoorType type;
    public bool isClosed;
    public bool lightOn;
    public bool enemyAtDoor;

    [SerializeField] private GameObject doorObj;
    [SerializeField] private GameObject lightObj;
    [SerializeField] private AudioSource doorCloseAudio;
    [SerializeField] private AudioSource doorOpenAudio;
    [SerializeField] private AudioSource enemySpottedAudio;
    private PowerManager powerManager;

    public enum DoorType
    {
        Left,
        Center,
        Right
    }

    private void Awake()
    {
        powerManager = FindAnyObjectByType<PowerManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        OpenDoor();
        TurnOffLight();
    }

    public void ToggleDoor()
    {
        isClosed = !isClosed;
        doorObj.SetActive(isClosed);
        if (isClosed)
        {
            doorCloseAudio.Play();
            powerManager.powerConsumers.Add(1);
        }
        else
        {
            doorOpenAudio.Play();
            powerManager.powerConsumers.Remove(1);
        }
    }

    public void CloseDoor()
    {
        isClosed = true;
        doorObj.SetActive(true);
        powerManager.powerConsumers.Add(1);
        doorCloseAudio.Play();
    }

    public void OpenDoor()
    {
        isClosed = false;
        doorObj.SetActive(false);
        powerManager.powerConsumers.Remove(1);
        doorOpenAudio.Play();
    }

    public void ToggleLight()
    {
        lightOn = !lightOn;
        lightObj.SetActive(lightOn);
        if (lightOn)
        {
            powerManager.powerConsumers.Add(1);
        }
        else
        {
            powerManager.powerConsumers.Remove(1);
        }

        if(lightOn && !isClosed && enemyAtDoor)
        {
            enemySpottedAudio.Play();
        }
    }

    public void TurnOnLight()
    {
        lightOn = true;
        lightObj.SetActive(true);
        powerManager.powerConsumers.Add(1);

        if(enemyAtDoor && !isClosed)
        {
            enemySpottedAudio.Play();
        }

        StartCoroutine(TurnOffLightAfter(1f));
    }

    public void TurnOffLight()
    {
        lightOn = false;
        lightObj.SetActive(false);
        powerManager.powerConsumers.Remove(1);
    }

    IEnumerator TurnOffLightAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        TurnOffLight();
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other.name + " has entered");
        if (other.tag == "Enemy" && !other.isTrigger)
        {
            enemyAtDoor = true;
        }
    }

    private void OnTriggerStay(Collider other )
    {
        //print(other.name + " has stayed");
        if (other.tag == "Enemy" && !other.isTrigger)
        {
            enemyAtDoor = true;
        }
    }

    private void OnTriggerExit(Collider other )
    {
        //print(other.name + " has left");
        if (other.tag == "Enemy" && !other.isTrigger)
        {
            enemyAtDoor = false;
        }
    }
}
