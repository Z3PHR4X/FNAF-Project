using System.Collections.Generic;
using UnityEngine;
using Zephrax.FNAFGame.Tools;


namespace Zephrax.FNAFGame.AI
{
    //Bonnie AI variant, teleports around the map, not tied to follow a certain path
    public class JumpEnemyAI : DefaultEnemyAI
    {
        public WaypointsManager waypointsManager;
        private List<DynamicWaypoints> jumpWaypoints;
        public int attackChance = 3;
        private int actPhase;

        public override void StartExt()
        {
            waypointsManager = FindAnyObjectByType<WaypointsManager>();
            jumpWaypoints = new List<DynamicWaypoints>();
            jumpWaypoints = waypointsManager.jumpPoints;
        }

        public override DynamicWaypoints SetNextWaypoint(List<DynamicWaypoints> potentialWaypoints)
        {
            DynamicWaypoints nextWaypoint;
            List<DynamicWaypoints> candidateWaypoints = new List<DynamicWaypoints>();
            List <DynamicWaypoints> tempWaypoints = new List<DynamicWaypoints>();

            state = AIState.Searching;
            foreach (var waypoint in potentialWaypoints) //Workaround for Unity deleting all references in scene
            {
                tempWaypoints.Add(waypoint);
            }

            if (!currentWaypoint.isAttackingPosition)
            {
                tempWaypoints.Clear();
                foreach (var waypoint in jumpWaypoints) //Workaround for Unity deleting all references in scene
                {
                    tempWaypoints.Add(waypoint);
                }

                //Ai type should not be able to instantly attack the player from their homeWaypoint
                if (currentWaypoint == homeWaypoint)
                {
                   for( int i = tempWaypoints.Count-1; i >= 0; i-- ) {
                        if (tempWaypoints[i].isAttackingPosition)
                        {
                            tempWaypoints.RemoveAt(i);
                        }
                    }
                    print($"{this.name} removed attacking waypoints from potential waypoints list");
                }

                //To keep chances of attacking a bit lower, use diceroll for forcing attack
                if (DiceRollGenerator.hasSuccessfulRoll(attackChance))
                {
                    for (int i = tempWaypoints.Count - 1; i >= 0; i--)
                    {
                        if (!tempWaypoints[i].isAttackingPosition)
                        {
                            tempWaypoints.RemoveAt(i);
                        }
                    }
                    print($"{this.name} is jumping to attack position");
                }
                else
                {
                    for (int i = tempWaypoints.Count - 1; i >= 0; i--)
                    {
                        if (tempWaypoints[i].isAttackingPosition)
                        {
                            tempWaypoints.RemoveAt(i);
                        }
                    }
                }

            }

            foreach (var waypoint in tempWaypoints)
            {
                if (!waypoint.isOccupied && waypoint != currentWaypoint)
                {
                    candidateWaypoints.Add(waypoint);
                }
            }
            if (candidateWaypoints.Count > 0)
            {
                int jumpIndex = Random.Range(0, candidateWaypoints.Count);
                nextWaypoint = candidateWaypoints[jumpIndex];
            }
            else
            {
                nextWaypoint = currentWaypoint;
                print("No available candidate waypoints found!");
            }

            print($"{this.name} has decided to move to: {nextWaypoint}");

            return nextWaypoint;
        }

        public override void UpdateActivityValues(int hour)
        {
            if(hour != actPhase)
            {
                actPhase = hour;
                attackChance += GetActivityValue();
            }
            base.UpdateActivityValues(hour);

        }
    }
}