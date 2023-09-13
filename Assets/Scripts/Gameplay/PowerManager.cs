using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PowerManager : MonoBehaviour
    {
        [Header("Settings")]
        public int powerReserve = 1000;
        public int powerUsageCost = 10;
        public int[] powerAdditionalDrainValues = new int[8];
        public float[] powerUsageIntervals = new float[5];
        public float[] powerAdditionalDrainIntervals = new float[8];
        [Header("In-game values")]
        public bool hasPower;
        public List<int> powerConsumers;
        public int powerUsage;
        private float powerUsageInterval;
        private double powerTimer, additonalDrainTimer;
        private float powerAdditionalDrainInterval;
        private int powerAddtionalDrain;

        // Start is called before the first frame update
        void Start()
        {
            hasPower = true;
            powerAdditionalDrainInterval = powerAdditionalDrainIntervals[Singleton.Instance.selectedNight - 1];
            powerAddtionalDrain = powerAdditionalDrainValues[Singleton.Instance.selectedNight - 1];

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

            //Additional powerdrain, might disable if it ends up being to hard
            if(additonalDrainTimer + powerAdditionalDrainInterval < Time.time)
            {
                powerReserve -= powerAddtionalDrain;
                additonalDrainTimer = Time.time;
            }

        }

    }
}