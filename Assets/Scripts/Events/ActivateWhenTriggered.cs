using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class ActivateWhenTriggered : MonoBehaviour
    {

        public GameObject objectToActivate;
        public string _tag;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == _tag)
            {
                //print(other.name + "has entered");
                objectToActivate.SetActive(true);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == _tag)
            {
                objectToActivate.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == _tag)
            {
                objectToActivate.SetActive(false);
            }
        }
    }
}
