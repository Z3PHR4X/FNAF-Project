using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zephrax.FNAFGame.Gameplay;

namespace Zephrax.FNAFGame.Tools
{
    public class SwapMaterialsOnCam : MonoBehaviour
    {
        [SerializeField] private Material defaultMaterial, cameraMaterial;
        private SkinnedMeshRenderer meshRenderer;
        private bool camState;

        // Start is called before the first frame update
        void Start()
        {
            meshRenderer = GetComponent<SkinnedMeshRenderer>();
            SwapMaterial(defaultMaterial);
        }

        // Update is called once per frame
        void Update()
        {
            if (camState != Player.Instance.isInCamera)
            {
                camState = Player.Instance.isInCamera;
                if (camState)
                {
                    SwapMaterial(cameraMaterial);
                }
                else
                {
                    SwapMaterial(defaultMaterial);
                }
            }
        }

        void SwapMaterial(Material mat)
        {
            meshRenderer.material = mat;
        }
    }
}