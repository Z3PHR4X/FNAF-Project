using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.InGame
{
    public class DisplayNight : MonoBehaviour
    {
        [SerializeField] private Text nightUi;

        // Start is called before the first frame update
        void Start()
        {
            nightUi.text = "Night " + (PlayerPrefs.GetInt("selectedNight") + 1);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}