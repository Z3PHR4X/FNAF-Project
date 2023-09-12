using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class ActivateWhenTriggered : MonoBehaviour
    {
        private void Start()
        {
            objectToActivate.SetActive(false);
        }

        public GameObject objectToActivate;
        public string tag;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(tag) && !other.isTrigger)
            {
                print(other.name + "has entered");
                objectToActivate.SetActive(true);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(tag) && !other.isTrigger)
            {
                objectToActivate.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(tag)&& !other.isTrigger)
            {
                objectToActivate.SetActive(false);
            }
        }
    }
}
