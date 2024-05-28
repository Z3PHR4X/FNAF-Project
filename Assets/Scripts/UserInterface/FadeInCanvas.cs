using UnityEngine;
using UnityEngine.UI;

namespace Zephrax.FNAFGame.UserInterface
{
    public class FadeInCanvas : MonoBehaviour
    {
        [Range(0, 1)][SerializeField] private float targetAlpha;
        [SerializeField] private float speed = 0.3f;
        private CanvasGroup canvas;
        private float alpha;

        [SerializeField] private bool FadeIn;


        private void Awake()
        {
            canvas = GetComponent<CanvasGroup>();
        }
        private void Start()
        {
            if (FadeIn)
            {
                alpha = 0f;
            }
            else
            {
                alpha = canvas.alpha;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (FadeIn)
            {
                if (alpha < targetAlpha)
                {
                    alpha = Mathf.Lerp(alpha, 1, speed * Time.deltaTime);
                    canvas.alpha = alpha;
                }
            }
            else
            {
                alpha = Mathf.Lerp(alpha, 0, speed * Time.deltaTime);
                canvas.alpha = alpha;
                if (alpha < targetAlpha)
                {
                    gameObject.SetActive(false);
                }
            }

        }
    }
}