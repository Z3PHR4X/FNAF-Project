using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

namespace AI
{
    public class DefaultEnemyAI : MonoBehaviour
    {
        /*Notes:
         *FNAF Source decompiled: https://imgur.com/a/xe01I#aaR227u
         * Each animatronic gets updated after a certain amount of time. 
         * Bonnie gets one every 4 and 97/100 seconds, Chica every 4 and 98/100 seconds, Freddy every 3 and 2/100 seconds, and Foxy every 5 and 1/100 seconds.
         * They are then chosen to move if a random number between 1 and 20 is less than or equal to the animatronic's current activity level. 
         * This makes them move more often the higher the activity level is and randomizes when they move.
         * Bonnie and Chica can move at the same time. The 1/100 sec offset makes Chica's check run one tick after Bonnie's check.
         * Foxy has a countdown time that's supposed to prevent him from moving until about 1-17.5 seconds after the monitor is put down. 
         * I'm not sure if it's intended or not, but since the countdown time is set when any camera is looked at and it's impossible for Foxy to move from Pirate Cove while the countdown time is greater than 0, viewing any camera keeps Foxy from attacking.
         */

        [Header("Settings")]
        public AnimatronicType animatronicType;
        public AIState state;
        [SerializeField] private Character characterData;
        public List<AIValues> aiValues;
        [SerializeField] private float triggerDetectionRange = 5f;
        public bool isBeingWatched;
        [SerializeField] private bool attacksWhenDistracted = false;
        [SerializeField] private bool hasMovementOffset = true;
        [Header("Setup")]
        public DynamicWaypoints homeWaypoint;
        //public List<DynamicWaypoints> waypointsInRange;
        [SerializeField] private AudioSource characterAudioSource;
        [SerializeField] private AudioClip defaultAudio, movementAudio, attackAudio, attackFailAudio;

        public DynamicWaypoints currentWaypoint;
        private int activityLevel;
        private float activityInterval; //keep track of how easily the AI can make a move
        private int activityPhase; //keeps track of which phase of the night the AI is in
        private double timeSinceLastAction, timeSinceLastSeen;
        private GameObject inCamerasView; //store camera that is viewing AI so it can be scrambled later
        private AIValues nightValues = new AIValues();
        private SphereCollider sphereCollider;
        private DynamicWaypoints nextWaypoint;
        private Door door;

        public enum AnimatronicType
        {
            Default,
            Rusher,
            Crawler
        }

        public enum AIState
        {
            Inactive,
            Hiding,
            Waiting,
            Watched,
            Searching,
            Moving,
            Attacking,
            Returning
        }

        private void Awake()
        {
            characterAudioSource = GetComponent<AudioSource>();
            characterAudioSource.volume = Singleton.Instance.sfxVolume;
            sphereCollider = gameObject.AddComponent<SphereCollider>();
            sphereCollider.isTrigger = true;
            sphereCollider.radius = triggerDetectionRange;
        }

        // Start is called before the first frame update
        void Start()
        {
            aiValues = characterData.aggressionProgression;
            nightValues = aiValues[Singleton.Instance.selectedNight-1];
            nightValues.DetermineStartValue();

            activityLevel = nightValues.activityValues[0];
            activityInterval = nightValues.actionInterval;
            currentWaypoint = homeWaypoint;
            currentWaypoint.isOccupied = true;
            timeSinceLastAction = Time.time;
            timeSinceLastSeen = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            //check if able to increase aggression level
            //increase aggressionlevel
            UpdateActivityValues(GameManagerV2.Instance.hour);
            CheckIfBeingWatched();

            //Main AI loop
            if (currentWaypoint.isAttackingPosition)
            {
                AttackState();
            }
            //every x seconds
            else if (timeSinceLastAction + activityInterval < Time.time && !currentWaypoint.isAttackingPosition)
            {
                //if roll succeeds
                if (DiceRollGenerator.hasSuccessfulRoll(activityLevel) && !isBeingWatched)
                {
                    //PlayAudio("default");
                    //decide where to move next
                    nextWaypoint = SetNextWayPoint(currentWaypoint.connectedWaypoints);
                    //move to next waypoint
                    if (nextWaypoint != null)
                    {
                        nextWaypoint.isOccupied = true;
                        Move(nextWaypoint, hasMovementOffset);
                    }
                    timeSinceLastAction = Time.time;
                }
                else if (isBeingWatched) {
                    timeSinceLastAction = Time.time + Random.Range(1, 17.5f);
                }
                else
                {
                    timeSinceLastAction = Time.time;
                }
            }
            else
            {
                state = AIState.Waiting;
            }
        }

        public virtual DynamicWaypoints SetNextWayPoint(List<DynamicWaypoints> potentialWaypoints)
        {
            state = AIState.Searching;
            nextWaypoint = null;
            int counter = 0;

            if(potentialWaypoints.Count > 0)
            {
                foreach (var waypoint in potentialWaypoints)
                {
                    if(currentWaypoint.flowWeight < waypoint.flowWeight)
                    {
                        nextWaypoint = waypoint;
                    }
                    else
                    {
                        counter++;
                    }
                }

                if (counter == potentialWaypoints.Count  || DiceRollGenerator.hasSuccessfulRoll(2))
                {
                    nextWaypoint = potentialWaypoints[Random.Range(0, potentialWaypoints.Count)];
                }
            }

            print($"{characterData.characterName} has decided to move to: {nextWaypoint}");

            return nextWaypoint;
        }

        public virtual void Move(DynamicWaypoints wayPoint, bool applyOffset)
        {
            state = AIState.Moving;
            //waypointsInRange.Clear();
            //move to waypoint location
            PlayAudio("movement");
            if (applyOffset)
            {
                gameObject.transform.position = wayPoint.transform.position + new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
            }
            else
            {
                gameObject.transform.position = wayPoint.transform.position;
            }
            gameObject.transform.rotation = wayPoint.transform.rotation;

            currentWaypoint.isOccupied = false;
            currentWaypoint = wayPoint;
            currentWaypoint.isOccupied = true;
            //scramble camera
        }

        public virtual void AttackState()
        {
            state = AIState.Attacking;

            if (timeSinceLastAction + nightValues.actionInterval < Time.time)
            {
                if (attacksWhenDistracted && Player.Instance.isInCamera && DiceRollGenerator.hasSuccessfulRoll(activityLevel))
                {
                    AttackPlayer();
                }
                else if (DiceRollGenerator.hasSuccessfulRoll(activityLevel))
                {
                    AttackPlayer();
                }
                timeSinceLastAction = Time.time;
            }

        }

        public virtual void AttackPlayer()
        {
            state = AIState.Attacking;
            //attempt to attack the player
            //if door is closed
            if (door != null)
            {
                if (door.isClosed)
                {
                    //Play audio and return home
                    door.BangOnDoor();
                    Move(homeWaypoint, false);
                    //lower power? 
                }
                else
                {
                    PlayAudio("attack");
                    //play attack/jumpscare animation

                    //once finished, player died
                    Player.Instance.isAlive = false;
                }
            }
            else
            {
                Move(homeWaypoint, false);
                print($"{characterData.characterName} tried to attack the player, but got confused as there was no door!");
            }

            timeSinceLastAction = Time.time;
        }

        public virtual void PlayAudio(string audioType)
        {
            switch (audioType)
            {
                case "attack":
                    characterAudioSource.clip = attackAudio;
                    characterAudioSource.Play();
                    break;

                case "attackFail":
                    characterAudioSource.clip = attackFailAudio;
                    characterAudioSource.Play();
                    break;

                case "movement":
                    characterAudioSource.clip = movementAudio;
                    characterAudioSource.Play();
                    break;

                default:
                    characterAudioSource.clip = defaultAudio;
                    characterAudioSource.Play();
                    break;
            }
        }

        public virtual void UpdateActivityValues(int hour)
        {
            if (activityPhase != hour)
            {
                activityPhase = hour;
                activityLevel += nightValues.activityValues[activityPhase];
                activityInterval = nightValues.actionInterval;
            }
        }

        public virtual void CheckIfBeingWatched()
        {
            //add code in case character doesnt want to be watched
        }

        public virtual void OnTriggerEnter(Collider other)
        {

            /*if (other.gameObject.CompareTag("Waypoint"))
            {
                //print(other);
                DynamicWaypoints waypoint = other.gameObject.GetComponent<DynamicWaypoints>();
                if (waypoint.waypointType.ToString() == animatronicType.ToString())
                {
                    if (!waypointsInRange.Contains(waypoint))
                    {
                        waypointsInRange.Add(waypoint);
                    }
                }
            }*/
            if (other.gameObject.CompareTag("Door")){
                door = other.gameObject.GetComponent<Door>();
            }
        }

        public virtual void OnTriggerStay(Collider other)
        {
            /*if (other.gameObject.CompareTag("Waypoint"))
            {
                //print(other);
                DynamicWaypoints waypoint = other.gameObject.GetComponent<DynamicWaypoints>();
                if (waypoint.waypointType.ToString() == animatronicType.ToString())
                {
                    if (!waypointsInRange.Contains(waypoint))
                    {
                        waypointsInRange.Add(waypoint);
                    }
                }
            }*/

            if (other.gameObject.CompareTag("Door"))
            {
                door = other.gameObject.GetComponent<Door>();
            }
        }

        public virtual void OnTriggerExit(Collider other)
        {
            /*if (other.gameObject.CompareTag("Waypoint"))
            {
                DynamicWaypoints waypoint = other.gameObject.GetComponent<DynamicWaypoints>();
                if (waypoint.waypointType.ToString() == animatronicType.ToString())
                {
                    waypointsInRange.Remove(waypoint);
                }
            }*/
            if (other.gameObject.CompareTag("Door"))
            {
                door = null;
            }
        }

        /* Spherecollider based waypoint detection 
         * 
         * Issues:
         * - More waypoints needed
         * - More waypoints in 1 area cause AI to get stuck in areas
         * - AI can teleport through walls 
         * 

        public virtual DynamicWaypoints SetNextWayPoint(List<DynamicWaypoints> potentialWaypoints)
        {
            state = AIState.Searching;

            //Remove current waypoint from list, so we wont remain stuck
            List<DynamicWaypoints> cleanup = new List<DynamicWaypoints>();
            foreach (var waypoint in potentialWaypoints)
            {
                if (waypoint == currentWaypoint || waypoint.isOccupied || waypoint.isAttackingPosition)
                {
                    cleanup.Add(waypoint);
                }
            }
            for(int x = 0; x < cleanup.Count; x++)
            {
                potentialWaypoints.Remove(cleanup[x]);
            }

            DynamicWaypoints nextWaypoint = null;
            if (potentialWaypoints.Count == 0)
            {
                waypointDetector.radius += 3;
            }

            if (potentialWaypoints.Count > 0)
            {
                int iteration = 0;
                nextWaypoint = potentialWaypoints[Random.Range(0, potentialWaypoints.Count)];
                while(nextWaypoint.isOccupied)
                {
                    if (iteration > 5) break;
                    waypointDetector.radius += 3;
                    nextWaypoint = potentialWaypoints[Random.Range(0, potentialWaypoints.Count)];
                    iteration++;
                }
                float nextWaypointDistance = 200;
                foreach (DynamicWaypoints waypoint in potentialWaypoints)
                {
                    if (!waypoint.isOccupied && !waypoint.isStartingPoint)
                    {
                        float waypointDistance = Vector3.Distance(currentWaypoint.transform.position, waypoint.transform.position);
                        if (nextWaypoint.flowWeight < waypoint.flowWeight)
                        {
                            nextWaypoint = waypoint;
                            nextWaypointDistance = waypointDistance;
                        }
                    }
                }
                waypointDetector.radius = waypointDetectionRange;
            }
            return nextWaypoint;
        }

        */
    }
}