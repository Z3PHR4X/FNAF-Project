using UnityEngine;
using UnityEngine.InputSystem;

namespace Zephrax.FNAFGame.Gameplay
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;
        //Controls
        public PlayerInputActions playerInputActions;
        public float cameraSpeed;
        //Setup
        public bool isInCamera, isAlive, isBeingAttacked;
        public PowerManager powerManager;
        public SecurityCameraManager securityCameraManager;
        public DoorManager doorManager;

        public Camera playerCamera;
        private Quaternion cameraRotation;
        [SerializeField] private Vector2 cameraBounds = new Vector2(-50, 50);

        private float mouseSensitivity;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            securityCameraManager = GetComponent<SecurityCameraManager>();
            playerInputActions = new PlayerInputActions();
        }

        // Start is called before the first frame update
        void Start()
        {
            isAlive = true;
            isInCamera = false;
            cameraRotation = playerCamera.transform.localRotation;
        }

        private void LateUpdate()
        {
            UpdateDiscord();
        }

        //InputSystem
        public void OnLook(InputValue context)
        {
           Vector2 lookValue = context.Get<Vector2>();
           UpdateCamera(lookValue.x);
        }

        private void Update()
        {
            UpdateCamera(cameraSpeed);
        }

        void UpdateCamera(float lookInput)
        {
            lookInput = lookInput * -1f;
            //when not paused
            if (Time.timeScale > 0)
            {
                if (!isInCamera)
                {
                    mouseSensitivity = Singleton.Instance.mouseSensitivity;
                    cameraRotation.y += (lookInput * mouseSensitivity) * Time.deltaTime;
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
}