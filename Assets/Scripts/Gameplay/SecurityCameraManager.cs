using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class SecurityCameraManager : MonoBehaviour
    {
        public bool isWatchingCameras;
        public List<SecurityCamera> securityCameras = new List<SecurityCamera>();
        public Canvas securityCameraInterface;

        [SerializeField] private Animator cameraNoiseController;
        [SerializeField] private AudioSource switchAudio;
        [SerializeField] private AudioSource turnOnAudio;
        [SerializeField] private AudioSource turnOffAudio;
        [SerializeField] private GameObject toggleWithCameras;

        private Camera mainCamera;
        private AudioListener mainAudioListener;
        private SecurityCamera currentCamera;
        private bool isPoweredDown;

        private void Awake()
        {
            mainCamera = Camera.main;
            mainAudioListener = mainCamera.GetComponent<AudioListener>();
        }

        // Start is called before the first frame update
        void Start()
        {
            isPoweredDown = false;
            isWatchingCameras = false;
            mainAudioListener.enabled = true;
            securityCameraInterface.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManagerV2.Instance.hasGameStarted && !GameManagerV2.Instance.hasPlayerWon)
            {
                if (Player.Instance.powerManager.hasPower)
                {
                    HandleInput();
                }
                else if (!isPoweredDown)
                {
                    PowerDown();
                }
            }
        }

        public void PowerDown()
        {
            ExitSecurityCamera();
            isPoweredDown=true;
        }

        public void ToggleSecurityCamera()
        {
            if(isWatchingCameras)
            {
                ExitSecurityCamera();
            }
            else
            {
                EnterSecurityCamera();
            }
        }

        public void EnterSecurityCamera()
        {
            toggleWithCameras.SetActive(false);
            isWatchingCameras = true;
            mainCamera.enabled = false;
            mainAudioListener.enabled = false;
            securityCameraInterface.gameObject.SetActive(true);
            if(currentCamera == null)
            {
                currentCamera = securityCameras[0];
            }

            currentCamera.ToggleCamera(true);
            turnOnAudio.Play();
            Player.Instance.isInCamera = true;
            Player.Instance.powerManager.powerConsumers.Add(1);
        }

        public void ExitSecurityCamera()
        {
            toggleWithCameras.SetActive(true);
            isWatchingCameras = false;
            mainCamera.enabled = true;
            mainAudioListener.enabled = true;
            securityCameraInterface.gameObject.SetActive(false);
            currentCamera.ToggleCamera(false);
            turnOffAudio.Play();
            Player.Instance.isInCamera = false;
            Player.Instance.powerManager.powerConsumers.Remove(1);
        }

        //Called from SecurityCamera button on UI
        public void SwitchCamera(SecurityCamera nextCamera)
        {
            currentCamera.ToggleCamera(false);
            currentCamera = nextCamera;
            currentCamera.ToggleCamera(true);
            switchAudio.Play();
        }

        private void HandleInput()
        {
            if(Input.GetKeyDown(KeyCode.Space)) {
            ToggleSecurityCamera();
            }
        }
    }
}