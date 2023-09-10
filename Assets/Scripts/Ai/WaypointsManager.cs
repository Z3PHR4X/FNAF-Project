using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class WaypointsManager : MonoBehaviour
    {
        public List<DynamicWaypoints> waypoints;
        private int currentTime;

        private void Awake()
        {
           //Get all DynamicWaypoints in the scene and add them to waypoints list            
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (GameManagerV2.Instance.nightTime != currentTime)
            {
                RecalculateWaypointFlow(GameManagerV2.Instance.nightTime, GameManagerV2.Instance.nightLength);
            }
        }

        private void RecalculateWaypointFlow(int curPhase, int maxPhase)
        {
            //Maybe have a pathfinding algorith get the fastest route to player
            //and set flowPriority accordingly
        }
    }
}
