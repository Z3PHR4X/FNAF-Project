using UnityEngine;

namespace Tools { 
    public class ActivateWhenOutOfCam : MonoBehaviour
    {
        [SerializeField] private GameObject activateObject;
        [RangeAttribute(0, 20)] public int chance;
        private bool camActive, isVisible;

        // Start is called before the first frame update
        void Start()
        {
            camActive = false;
            activateObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Player.Instance.isInCamera != camActive)
            {
                if (Player.Instance.isInCamera == false)
                {
                    if (DiceRollGenerator.hasSuccessfulRoll(chance))
                    {
                        isVisible = true;
                        activateObject.SetActive(true);
                    }
                }
                else if(Player.Instance.isInCamera == true && isVisible) {
                    isVisible = false;
                    activateObject.SetActive(false);
                }

                camActive = Player.Instance.isInCamera;
            }
        }
    }
}