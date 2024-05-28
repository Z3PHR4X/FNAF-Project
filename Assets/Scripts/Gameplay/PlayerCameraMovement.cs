using System.Collections.Generic;
using UnityEngine;

namespace Zephrax.FNAFGame.Gameplay
{
    public class PlayerCameraMovement : MonoBehaviour
    {
        public PlayerInputActions playerInputActions;
        [Header("Camera Settings")]
        [Tooltip("Only provide offsets for 1 side, the offset will be applied on both screen edges. (ex. 0.25)")]
        [Range(0,0.5f)][SerializeField] private List<float> movementZoneOffsets;
        [Tooltip("Only provide equal number of speeds as offsets.")]
        [SerializeField] private List<float> cameraZoneSpeed;
        private Player player;


        private void Start()
        {
            movementZoneOffsets.Sort();
            cameraZoneSpeed.Sort();
            cameraZoneSpeed.Reverse();
            player = Player.Instance;
        }

        private void Update()
        {
            Vector2 mousePos = Input.mousePosition;

            for (int i = 0; i < movementZoneOffsets.Count; i++)
            {
                float movementZoneSize = movementZoneOffsets[i] * Screen.width;
                if (mousePos.x < movementZoneSize)
                {
                    player.cameraSpeed = cameraZoneSpeed[i];
                    break;
                }
                else if (mousePos.x > Screen.width - movementZoneSize)
                {
                    player.cameraSpeed = cameraZoneSpeed[i] * -1;
                    break;
                }
                else
                {
                    player.cameraSpeed = 0;
                }
            }
        }
        
    }
}