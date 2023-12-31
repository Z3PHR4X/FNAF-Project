using UnityEngine;
using Gameplay;
using Discord;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public bool isInCamera, isAlive, isBeingAttacked;
    public PowerManager powerManager;
    public SecurityCameraManager securityCameraManager;
    public DoorManager doorManager;

    public Camera playerCamera;
    private Quaternion cameraRotation;
    [SerializeField] private float cameraKeySpeed = 60f;
    [SerializeField] private Vector2 cameraBounds = new Vector2(-60,60);

    private float mouseSensitivity;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        //playerCamera = GetComponentInChildren<Camera>();
        //GameManagerV2.Instance.currentCam = playerCamera;
        securityCameraManager = GetComponent<SecurityCameraManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        isInCamera = false;
        cameraRotation = playerCamera.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCamera();
    }

    private void LateUpdate()
    {
        UpdateDiscord();
    }

    void UpdateCamera()
    {
        //when not paused
        if (Time.timeScale > 0)
        {
            if (!isInCamera)
            {
                mouseSensitivity = Singleton.Instance.mouseSensitivity;
                cameraRotation.y += Input.GetAxis("Mouse X") * mouseSensitivity;
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    cameraRotation.y += 1 * cameraKeySpeed;
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    cameraRotation.y -= 1 * cameraKeySpeed;
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    cameraRotation.y = 0;
                }
                cameraRotation.y = Mathf.Clamp(cameraRotation.y, cameraBounds.x, cameraBounds.y);

                playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, cameraRotation.z);
            }
        }
    }

    void UpdateDiscord()
    {
        if (Singleton.Instance.discord)
        {
            if (Singleton.Instance.discord.enabledRichPresence)
            {
                string powerUsageText = "";
                for (int i = 0; i < powerManager.powerUsage; i++)
                {
                    powerUsageText += "/";
                }
                float powerReserveText = (powerManager.powerReserve * 10) / 100;
                int timeText = GameManagerV2.Instance.hour;
                if (timeText == 0) timeText = 12;
                Singleton.Instance.discord.UpdateStatus($"Power reserve: {powerReserveText}% - Usage: [{powerUsageText}]", $"Surviving Night {Singleton.Instance.selectedNight} - {timeText}AM", "devbears", Singleton.Instance.selectedMap.levelName);
            }
        }
    }
}
