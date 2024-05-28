using UnityEngine;

namespace Zephrax.FNAFGame.Tools
{
    public class OpenURL : MonoBehaviour
    {
        public void OpenURLInBrowser(string inputUrl)
        {
            Application.OpenURL(inputUrl);
        }
    }
}
