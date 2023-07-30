using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class FreddyNose : MonoBehaviour
    {
        [SerializeField] private AudioSource noseAudio;
        [SerializeField] private Camera camera;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            { // if left button pressed...
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && hit.collider.CompareTag("Freddy Nose"))
                    {
                        noseAudio.Play();
                    }
                }
            }
        }
    }
}
