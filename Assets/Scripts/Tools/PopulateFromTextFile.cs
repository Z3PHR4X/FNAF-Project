using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zephrax.FNAFGame.Tools
{
    public class PopulateFromTextFile : MonoBehaviour
    {
        public Text text;
        public TextAsset textFile;

        // Start is called before the first frame update
        void Start()
        {
            text.text = textFile.text;
        }
    }
}