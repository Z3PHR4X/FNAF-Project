using UnityEngine;

namespace Tools
{
    public class OpenURL : MonoBehaviour
    {
        public void OpenURLInBrowser(string inputUrl)
        {
            Application.OpenURL(inputUrl);
        }
    }
}
