using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace Zephrax.FNAFGame.Gameplay
{
    public class InteractableObject : MonoBehaviour
    {
        public PlayerInputActions playerInputActions;
        private PlayerInput playerInput;
        [SerializeField] private string raycastTag;
        public UnityEvent interactionEvent;
        
        private Camera cam;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            cam = Camera.main;
            playerInput.camera = cam;
            playerInput.uiInputModule = FindAnyObjectByType<EventSystem>().GetComponent<InputSystemUIInputModule>();
        }

        //TODO: Look into more efficient implementation
        public void OnInteract(InputValue context)
        {
            if (context.isPressed && GameManagerV2.Instance.isGameplayActive(true))
            { // if left button pressed...
                //print("Interact!");

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    //print($"hit! {hit.transform.tag}!");
                    Color color = Color.cyan;
                    Debug.DrawRay(cam.transform.position, hit.transform.position, color);
                    if (hit.collider != null && hit.collider.CompareTag(raycastTag) && hit.collider.gameObject == this.gameObject)
                    {
                        //print("Click!");
                        interactionEvent.Invoke();
                    }
                    
                }
            }
        }
    }
}
