using UnityEngine;
using Gameplay;

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
}
