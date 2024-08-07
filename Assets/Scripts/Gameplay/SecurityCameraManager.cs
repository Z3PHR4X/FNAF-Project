using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Zephrax.FNAFGame.Gameplay
{
    public class SecurityCameraManager : MonoBehaviour
    {
        public PlayerInputActions playerInputActions;

        public bool isWatchingCameras;
        public List<SecurityCamera> securityCameras = new List<SecurityCamera>();
        public Canvas securityCameraInterface;

        [SerializeField] private GameObject transitionNoise;
        [SerializeField] private AudioSource switchAudio;
        [SerializeField] private AudioSource turnOnAudio;
        [SerializeField] private AudioSource turnOffAudio;
        [SerializeField] private GameObject toggleWithCameras;

        private bool watchingVents;
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
                if (!Player.Instance.powerManager.hasPower&& !isPoweredDown)
                {
                    PowerDown();
                }
            }
            else if(GameManagerV2.Instance.hasGameStarted && GameManagerV2.Instance.hasPlayerWon)
            { 
                ExitSecurityCamera(true);
            }
        }

        public void PowerDown()
        {
            if(Player.Instance.isInCamera)
            {
                ExitSecurityCamera(false);
            }
            
            isPoweredDown=true;
        }

        public void ToggleSecurityCamera()
        {
            if (!isPoweredDown && !Player.Instance.isBeingAttacked)
            {
                if (isWatchingCameras)
                {
                    ExitSecurityCamera(false);
                }
                else
                {
                    EnterSecurityCamera();
                }
            }
        }

        public void EnterSecurityCamera()
        {
            StartCoroutine(CameraTransition(0.2f));
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

        public void ExitSecurityCamera(bool quietly)
        {
            //Check prevents errors occuring in cases where night is completed/player dies when cam was never used.
            if (currentCamera != null)
            {
                toggleWithCameras.SetActive(true);
                isWatchingCameras = false;
                mainCamera.enabled = true;
                mainAudioListener.enabled = true;
                securityCameraInterface.gameObject.SetActive(false);
                currentCamera.ToggleCamera(false);
                if (!quietly)
                {
                    turnOffAudio.Play();
                }
                Player.Instance.isInCamera = false;
                Player.Instance.powerManager.powerConsumers.Remove(1);
                //GameManagerV2.Instance.currentCam = Player.Instance.playerCamera;
            }
        }

        //Called from SecurityCamera button on UI
        public void SwitchCamera(SecurityCamera nextCamera)
        {
            StartCoroutine(CameraTransition(0.2f));
            currentCamera.ToggleCamera(false);
            currentCamera = nextCamera;
            currentCamera.ToggleCamera(true);
            switchAudio.Play();
        }

        public void OnToggleInterface(InputValue context)
        {
            if (Player.Instance.powerManager.hasPower && GameManagerV2.Instance.isGameplayActive(false))
            {
                ToggleSecurityCamera();
            }
        }

        public void ScrambleCamera()
        {
            StartCoroutine(CameraTransition(0.5f));
        }

        IEnumerator CameraTransition(float duration)
        {
            transitionNoise.SetActive(true);
            yield return new WaitForSeconds(duration);
            transitionNoise.SetActive(false);

        }
    }
}