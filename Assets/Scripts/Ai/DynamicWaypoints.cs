using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class DynamicWaypoints : MonoBehaviour
    {
        public List<DynamicWaypoints> connectedWaypoints = new List<DynamicWaypoints>();
        public AIValues waypointFlowValues;
        public int flowWeight = 1;
        public bool isStartingPoint = false;
        public bool isCrawling = false;
        public bool isOccupied = false;
        public bool isAttackingPosition = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}