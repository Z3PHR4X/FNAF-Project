using UnityEngine;

namespace Zephrax.FNAFGame.SceneSwitching {
    public class OverlayController : MonoBehaviour
    {
        private GameObject controller;

        private void Start()
        {
            controller = GameObject.Find("Controller");
        }

    }
}