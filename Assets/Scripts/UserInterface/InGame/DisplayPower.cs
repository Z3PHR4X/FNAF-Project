using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.InGame
{
    public class DisplayPower : MonoBehaviour
    {
        [SerializeField] private Text powerText;
        float powerReserve;

        // Start is called before the first frame update
        void Start()
        {
            powerReserve = Player.Instance.powerManager.powerReserve;
            UpdatePowerDisplay(powerReserve);
        }

        // Update is called once per frame
        void Update()
        {
            if(powerReserve != Player.Instance.powerManager.powerReserve)
            {
                powerReserve = Player.Instance.powerManager.powerReserve;
                UpdatePowerDisplay(powerReserve);
            }
        }

        void UpdatePowerDisplay(float power)
        {
            int displayPower = Mathf.RoundToInt(power);
            powerText.text = $"Power reserve: {displayPower}%";
        }
    }
}