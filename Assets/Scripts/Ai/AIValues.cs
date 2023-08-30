using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [System.Serializable]
    public class AIValues
    {
        public bool hasRandomStart = false;
        public Vector2 randomStartRange;
        public List<int> activityValues = new List<int>(7);
        public float actionInterval;
    }
}