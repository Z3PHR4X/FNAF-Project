using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUsageDisplay : MonoBehaviour
{
    [SerializeField] private Image powerUsageDisplay;
    private int powerUsage;

    // Start is called before the first frame update
    void Start()
    {
        powerUsage = 1;
        UpdatePowerUsageDisplay(powerUsage);
    }

    // Update is called once per frame
    void Update()
    {
        if(powerUsage != Player.Instance.powerManager.powerUsage)
        {
            powerUsage = Player.Instance.powerManager.powerUsage;
            UpdatePowerUsageDisplay(powerUsage);
        }
    }

    void UpdatePowerUsageDisplay(int powerUsage)
    {
        powerUsageDisplay.fillAmount = 0.2f * powerUsage;
    }
}
