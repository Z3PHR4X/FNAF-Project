using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PowerManager : MonoBehaviour
    {
        [Header("Settings")]
        public float powerReserve;
        public float powerUsageCost;
        public float[] powerUsageIntervals = new float[5];
        [Header("In-game values")]
        public bool hasPower;
        public List<int> powerConsumers;
        public int powerUsage;
        private float powerUsageInterval;
        private double powerTimer;

        // Start is called before the first frame update
        void Start()
        {
            hasPower = true;
            powerConsumers = new List<int>();
            powerTimer = Time.time;
            UpdatePower();
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManagerV2.Instance.hasGameStarted && !GameManagerV2.Instance.hasPlayerWon)
            {
                if (powerReserve > 0)
                {
                    UpdatePower();
                }
                else
                {
                    hasPower = false;
                    powerReserve = 0;
                    powerUsage = 0;
                }
            }
        }

        void UpdatePower()
        {
            if (powerConsumers.Count == 0)
            {
                powerConsumers.Add(1);
            }
            powerUsage = powerConsumers.Count;
            powerUsage = Mathf.Clamp(powerUsage, 0, powerUsageIntervals.Length);
            powerUsageInterval = powerUsageIntervals[powerUsage-1];
           
            if(powerTimer + powerUsageInterval < Time.time) {

                powerReserve -= powerUsageCost;
                powerTimer = Time.time;
            }
        }

    }
}