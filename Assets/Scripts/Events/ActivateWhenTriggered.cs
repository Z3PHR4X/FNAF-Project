using UnityEngine;

namespace Zephrax.FNAFGame.Events
{
    public class ActivateWhenTriggered : MonoBehaviour
    {
        private void Start()
        {
            objectToActivate.SetActive(false);
        }

        public GameObject objectToActivate;
        public string activateOnTag;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(activateOnTag) && !other.isTrigger)
            {
                print(other.name + "has entered");
                objectToActivate.SetActive(true);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(activateOnTag) && !other.isTrigger)
            {
                objectToActivate.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(activateOnTag) && !other.isTrigger)
            {
                objectToActivate.SetActive(false);
            }
        }
    }
}
