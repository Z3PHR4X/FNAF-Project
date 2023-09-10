using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public bool isInCamera, isAlive, hasPower;
    public PowerManager powerManager;

    private Camera playerCamera;
    private Quaternion cameraRotation;
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

        playerCamera = GetComponentInChildren<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isInCamera = false;
        cameraRotation = playerCamera.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            if (!isInCamera)
            {
                mouseSensitivity = Singleton.Instance.mouseSensitivity;
                cameraRotation.y += Input.GetAxis("Mouse X") * mouseSensitivity;
                cameraRotation.y = Mathf.Clamp(cameraRotation.y, cameraBounds.x, cameraBounds.y);

                playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, cameraRotation.z);
            }
        }
    }
}
