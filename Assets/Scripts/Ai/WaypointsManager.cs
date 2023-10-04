using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class WaypointsManager : MonoBehaviour
    {
        public List<DynamicWaypoints> waypoints;
        private int currentTime;
        private GameManagerV2 gameManager;

        private void Awake()
        {
            //Get all DynamicWaypoints in the scene and add them to waypoints list
        }

        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameManagerV2.Instance;
            RecalculateWaypointFlow(gameManager.hour,gameManager.night);
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManagerV2.Instance.hour != currentTime)
            {
                RecalculateWaypointFlow(gameManager.hour, gameManager.night);
                currentTime = gameManager.hour;
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
