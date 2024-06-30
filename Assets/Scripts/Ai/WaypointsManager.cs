using System.Collections.Generic;
using UnityEngine;
using Zephrax.FNAFGame.Gameplay;

namespace Zephrax.FNAFGame.AI
{
    public class WaypointsManager : MonoBehaviour
    {
        [Header("Regular AI movement")]
        public List<DynamicWaypoints> waypoints;
        [Header("Jump AI movement")]
        public List<DynamicWaypoints> jumpPoints;
        private int currentTime, currentNight;

        private void Awake()
        {
            //Get all DynamicWaypoints in the scene and add them to waypoints list
        }

        // Start is called before the first frame update
        void Start()
        {
            currentTime = 0;
            currentNight = Singleton.Instance.selectedNight;
            RecalculateWaypointFlow(currentTime, currentNight);
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManagerV2.Instance.hour != currentTime)
            {
                currentTime = GameManagerV2.Instance.hour;
                RecalculateWaypointFlow(currentTime, currentNight);
                
            }
        }

        private void RecalculateWaypointFlow(int curPhase, int curNight)
        {
            //Maybe have a pathfinding algorith get the fastest route to player
            //and set flowPriority accordingly

            foreach (DynamicWaypoints waypoint in waypoints)
            {
                waypoint.UpdateFlowWeight(curPhase, curNight);
            }
        }
    }
}
