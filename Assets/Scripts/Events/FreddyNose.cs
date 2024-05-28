using UnityEngine;
using Zephrax.FNAFGame.Gameplay;

namespace Zephrax.FNAFGame.Events
{
    public class FreddyNose : MonoBehaviour
    {
        private AudioSource noseAudio;
        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
            noseAudio = GetComponentInChildren<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !Player.Instance.isInCamera && !GameManagerV2.Instance.isPaused)
            { // if left button pressed...

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && hit.collider.CompareTag("Freddy Nose") && hit.collider.gameObject == this.gameObject)
                    {
                        noseAudio.Play();
                    }
                }
            }
        }
    }
}
