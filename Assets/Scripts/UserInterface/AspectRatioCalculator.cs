using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zephrax.FNAFGame.UserInterface
{

    public class AspectRatioCalculator : MonoBehaviour
    {

        [SerializeField] private AspectRatioFitter aspectRatio;
        [SerializeField] private Image image;

        // Start is called before the first frame update
        void Start()
        {
            if (aspectRatio == null)
            {
                aspectRatio = GetComponent<AspectRatioFitter>();
            }
            if (image == null)
            {
                image = GetComponent<Image>();
            }

            float ratio;
            ratio = ((float)image.sprite.texture.width / (float)image.sprite.texture.height);

            aspectRatio.aspectRatio = ratio;
        }
    }
}