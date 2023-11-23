using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Events
{
    public class WinSceneController : MonoBehaviour
    {
        [SerializeField] private TMP_Text completionText;
        [TextArea][SerializeField] private string night5CompletionText, night7CompletionText;
        [SerializeField] private Image completionRewardImage;
        [SerializeField] private Sprite night5CompletionSprite, night7CompletionSprite;

        // Start is called before the first frame update
        void Start()
        {
            if(Singleton.Instance.selectedNight == 5)
            {
                completionText.text = night5CompletionText;
                completionRewardImage.sprite = night5CompletionSprite;
            }
            else
            {
                completionText.text = night7CompletionText;
                completionRewardImage.sprite = night7CompletionSprite;
            }
        }
    }
}
