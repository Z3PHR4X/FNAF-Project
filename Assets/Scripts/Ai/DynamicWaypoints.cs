using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class DynamicWaypoints : MonoBehaviour
    {
        public WaypointType waypointType;
        public List<DynamicWaypoints> connectedWaypoints = new List<DynamicWaypoints>();
        public List<AIValues> waypointFlowValues = new List<AIValues>(1);
        public int flowWeight = 1;
        public bool isStartingPoint = false;
        public bool forceNoOffset = false;
        public bool isCrawling = false;
        public bool isOccupied = false;
        public bool isAttackingPosition = false;

        public enum WaypointType
        {
            Default,
            Rusher,
            Crawler
        }

        private void Start()
        {
            flowWeight = 0;
        }

        public void UpdateFlowWeight(int hour, int night)
        {
            int newFlowWeight = 0;
            AIValues nightFlowValues;
            if (waypointFlowValues.Count > night - 1)
            {
                nightFlowValues = waypointFlowValues[0];
            }
            else
            {
                nightFlowValues = waypointFlowValues[waypointFlowValues.Count - 1];
            }
            if (nightFlowValues.activityValues.Count > hour)
            {
                newFlowWeight = nightFlowValues.activityValues[hour];
            }
            else
            {
                newFlowWeight = 0;
            }

            flowWeight += newFlowWeight;
        }
    }
}