using TMPro;
using UnityEngine;

namespace Zephrax.FNAFGame.UserInterface
{
    public class DisplayVersion : MonoBehaviour
    {
        [SerializeField] private TMP_Text versionText;
        [SerializeField] private bool preppendVersionText;
        [SerializeField] private bool appendBuildDate = true;

        // Start is called before the first frame update
        void Start()
        {
            if (versionText != null)
            {
                versionText.text = "";
                if (preppendVersionText)
                {
                    versionText.text = "Version: ";
                }
                versionText.text += Application.version;
                if (appendBuildDate)
                {
                    if (Application.isEditor)
                    {
                        versionText.text += $" [UnityDev]";
                    }
                    else
                    {
                        versionText.text += $" Build: {BuildInfo.BUILD_TIME}";
                    }
                }

            }
        }
    }
}