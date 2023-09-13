using UnityEngine;

namespace Gameplay
{
    public class SecurityCamera : MonoBehaviour
    {
        public Camera cameraComponent;
        public AudioListener audioListener;
        public Light cameraLight;
        [SerializeField] private GameObject enableWithCamera;
        public bool isBeingUsed;
        [SerializeField]
        private int _rotationSpeed = 2;
        [SerializeField]
        private int _maxRotation = 60;

        private float rotationOffset;

        private void Awake()
        {
            cameraComponent = GetComponent<Camera>();
            audioListener = GetComponent<AudioListener>();
            cameraLight = GetComponentInChildren<Light>();
        }

        // Start is called before the first frame update
        void Start()
        {
            isBeingUsed = false;
            if (cameraComponent != null)
            {
                cameraComponent.enabled = false;
            }
            if (audioListener != null)
            {
                audioListener.enabled = false;
            }
            if (cameraLight != null)
            {
                cameraLight.enabled = false;
            }
            if(enableWithCamera != null)
            {
                enableWithCamera.SetActive(false);
            }
            rotationOffset = transform.eulerAngles.y;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateRotation();
        }

        public void ToggleCamera(bool isOn)
        {
            isBeingUsed = isOn;
            if (cameraComponent != null)
            {
                cameraComponent.enabled = isOn;
            }
            if (audioListener != null)
            {
                audioListener.enabled = isOn;
            }
            if (cameraLight != null)
            {
                cameraLight.enabled = isOn;
            }
            if (enableWithCamera != null)
            {
                enableWithCamera.SetActive(isOn);
            }
        }

        private void UpdateRotation()
        {
            transform.rotation = Quaternion.Euler(28, rotationOffset + Mathf.PingPong(Time.time * _rotationSpeed, _maxRotation), 0);
        }
    }
}