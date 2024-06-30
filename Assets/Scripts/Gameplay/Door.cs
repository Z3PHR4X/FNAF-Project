using System.Collections;
using UnityEngine;

namespace Zephrax.FNAFGame.Gameplay
{
    public class Door : MonoBehaviour
    {
        public DoorType type;
        public AnimatronicType animatronicType;
        public bool isEnabled;
        public bool isClosed;
        public bool lightOn;
        public bool enemyAtDoor;

        [SerializeField] private GameObject doorObj;
        [SerializeField] private GameObject lightObj;
        [Header("Door button material")]
        [SerializeField] private MeshRenderer doorButtonRenderer;
        [SerializeField] private Color doorDefaultColor;
        [SerializeField] private Color doorEnabledColor;
        [SerializeField] private Color doorDisabledColor;
        [Header("Light button material")]
        [SerializeField] private MeshRenderer lightButtonRenderer;
        [SerializeField] private Color lightDefaultColor;
        [SerializeField] private Color lightEnabledColor;
        [SerializeField] private Color lightDisabledColor;

        [Header("Audio")]
        [SerializeField] private AudioSource doorCloseAudio;
        [SerializeField] private AudioSource doorOpenAudio;
        [SerializeField] private AudioSource doorBangingAudio;
        [SerializeField] private AudioSource doorDisabledAudio;
        [SerializeField] private AudioSource enemySpottedAudio;
        private PowerManager powerManager;
        private Material doorButtonMat;

        public enum DoorType
        {
            Left,
            Center,
            Right
        }

        public enum AnimatronicType
        {
            Default,
            Rusher,
            Crawler
        }

        private void Awake()
        {
            powerManager = FindAnyObjectByType<PowerManager>();
        }

        // Start is called before the first frame update
        void Start()
        {
            doorButtonMat = doorButtonRenderer.material;
            isEnabled = true;
            OpenDoor(true);
            TurnOffLight();
        }

        public void BangOnDoor()
        {
            doorBangingAudio.Play();
        }

        public void ToggleDoor()
        {
            if (isEnabled)
            {
                isClosed = !isClosed;
                doorObj.SetActive(isClosed);
                if (isClosed)
                {
                    doorCloseAudio.Play();
                    doorButtonMat.SetColor("_EmissionColor", doorEnabledColor);
                    powerManager.powerConsumers.Add(1);
                }
                else
                {
                    doorOpenAudio.Play();
                    doorButtonMat.SetColor("_EmissionColor", doorDefaultColor);
                    powerManager.powerConsumers.Remove(1);
                }
            }
            else
            {
                doorDisabledAudio.Play();
            }
        }

        public void CloseDoor()
        {
            isClosed = true;
            doorObj.SetActive(true);
            powerManager.powerConsumers.Add(1);
            doorCloseAudio.Play();
            doorButtonMat.SetColor("_EmissionColor", doorEnabledColor);
        }

        public void OpenDoor()
        {
            isClosed = false;
            doorObj.SetActive(false);
            powerManager.powerConsumers.Remove(1);
            doorOpenAudio.Play();
            doorButtonMat.SetColor("_EmissionColor", doorDefaultColor);
        }

        public void OpenDoor(bool quietly)
        {
            isClosed = false;
            doorObj.SetActive(false);
            powerManager.powerConsumers.Remove(1);
            doorButtonMat.SetColor("_EmissionColor", doorDefaultColor);
        }

        public void DisableDoor()
        {
            isEnabled = false;
            doorButtonMat.SetColor("_EmissionColor", doorDisabledColor);
        }

        public void ToggleLight()
        {
            if (lightOn)
            {
                TurnOffLight();
            }
            else
            {
                TurnOnLight();
            }

            if (lightOn && !isClosed && enemyAtDoor)
            {
                enemySpottedAudio.Play();
            }
        }

        public void TurnOnLight()
        {
            if (!lightOn)
            {
                lightOn = true;
                lightObj.SetActive(true);
                powerManager.powerConsumers.Add(1);

                if (enemyAtDoor && !isClosed)
                {
                    enemySpottedAudio.Play();
                }

                StartCoroutine(TurnOffLightAfter(1f));
            }
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

        private void OnTriggerStay(Collider other)
        {
            //print(other.name + " has stayed");
            if (other.tag == "Enemy" && !other.isTrigger)
            {
                enemyAtDoor = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //print(other.name + " has left");
            if (other.tag == "Enemy" && !other.isTrigger)
            {
                enemyAtDoor = false;
            }
        }
    }

}