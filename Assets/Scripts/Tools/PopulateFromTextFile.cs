using UnityEngine;
using TMPro;

namespace Zephrax.FNAFGame.Tools
{
    public class PopulateFromTextFile : MonoBehaviour
    {
        public TMP_Text text;
        public TextAsset textFile;

        // Start is called before the first frame update
        void Start()
        {
            text.text = textFile.text;
        }
    }
}