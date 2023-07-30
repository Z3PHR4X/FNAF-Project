using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    public GameObject freddyNose;
    [SerializeField] private float sensitivity;
    public bool isInCamera;

    private Quaternion cameraRotation;
    private float lookRightMax = 60;
    private float lookLeftMax = -60;
    
    // Start is called before the first frame update
    void Start()
    {
        sensitivity = PlayerPrefs.GetFloat("mouseSensitivity");
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
                freddyNose.SetActive(true);
                cameraRotation.y += Input.GetAxis("Mouse X") * sensitivity;
                cameraRotation.y = Mathf.Clamp(cameraRotation.y, lookLeftMax, lookRightMax);
            
                playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, cameraRotation.z);
            }
            else
            {
                freddyNose.SetActive(false);
            }
        }
    }

    public void UpdateSensitivity()
    {
        sensitivity = PlayerPrefs.GetFloat("mouseSensitivity");
    }
}
