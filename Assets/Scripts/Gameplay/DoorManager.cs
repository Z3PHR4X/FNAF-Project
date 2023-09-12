using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public Door leftDoor;
    public Door centerDoor;
    public Door rightDoor;

    private List<Door> doors;
    private PowerManager powerManager;
    private bool isPoweredDown = false;

    private void Awake()
    {
        powerManager = GetComponent<PowerManager>();
    }

    private void Start()
    {
        doors = new List<Door>
        {
            leftDoor,
            centerDoor,
            rightDoor
        };
    }

    private void Update()
    {
        if (powerManager.hasPower)
        {
            HandleInput();
        }
        else if(!isPoweredDown) 
        {
            PowerDown();
        }
    }

    void PowerDown()
    {
        foreach (Door door in doors)
        {
            door.TurnOffLight();
            door.OpenDoor();
        }
        isPoweredDown=true;
    }
    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            leftDoor.ToggleDoor();
        }
        if(Input.GetKeyDown(KeyCode.S))
        { 
            centerDoor.ToggleDoor();
        }
        if (Input.GetKeyDown(KeyCode.D))
        { 
            rightDoor.ToggleDoor(); 
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            leftDoor.TurnOnLight();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            centerDoor.TurnOnLight();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            rightDoor.TurnOnLight();
        }
    }
}
