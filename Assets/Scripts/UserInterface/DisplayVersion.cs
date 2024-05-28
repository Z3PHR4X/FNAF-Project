using UnityEngine;
using UnityEngine.UI;

namespace Zephrax.FNAFGame.UserInterface
{
    public class DisplayVersion : MonoBehaviour
    {
        [SerializeField] private Text versionText;

        // Start is called before the first frame update
        void Start()
        {
            if (versionText != null)
            {
                versionText.text = "Version: " + Application.version;
            }
        }
    }
}