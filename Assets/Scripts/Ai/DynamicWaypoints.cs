using System.Collections;
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
        public bool isCrawling = false;
        public bool isOccupied = false;
        public bool isAttackingPosition = false;

        public enum WaypointType
        {
            Default,
            Rusher,
            Crawler
        }

        public void UpdateFlowWeight(int hour, int night)
        {
            int newFlowWeight = 0;
            AIValues nightFlowValues = waypointFlowValues[night-1];
            newFlowWeight = nightFlowValues.activityValues[hour];
    }
    }
}