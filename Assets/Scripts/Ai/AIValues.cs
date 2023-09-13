using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [System.Serializable]
    public class AIValues
    {
        public bool isEnabled = true;
        public bool hasRandomStart = false;
        public Vector2 randomStartRange;
        public List<int> activityValues = new List<int>(7);
        public float actionInterval;

        public void DetermineStartValue()
        {
            if(hasRandomStart)
            {
                int min = (int)randomStartRange.x;
                int max = (int)randomStartRange.y+1;
                activityValues[0] = Random.Range(min, max);
            }
        }
    }
}