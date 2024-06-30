using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Zephrax.FNAFGame.Tools;
using Zephrax.FNAFGame.Gameplay;

namespace Zephrax.FNAFGame.AI
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
        private int activityLevel;
        [Range(0,20)] [SerializeField] private int randomAudioChance;
        [SerializeField] private float randomAudioInterval;
        public bool isBeingWatched;
        [SerializeField] private bool attacksWhenDistracted = false;
        [SerializeField] private bool hasMovementOffset = true;
        [SerializeField][Range(0, 20)] int lookChance;
        [SerializeField][Range(0, 20)] int enterOfficeChance;
        [SerializeField] private bool entersOffice = false;
        [Header("Setup")]
        public DynamicWaypoints homeWaypoint;
        public AudioSource characterAudioSource;
        public AudioSource characterRandomAudioSource;
        public AudioClip defaultAudio, movementAudio, attackAudio, attackFailAudio;
        private LookAtCurrentCamera lookAtCurrentCamera;
        [SerializeField] private VideoPlayer jumpscareVideo;
        [SerializeField] private SkinnedMeshRenderer eyeRenderer;
        [SerializeField] private Material defaultEyeMaterial, possessedEyeMaterial;

        private DynamicWaypoints dummyWaypoint;
        public DynamicWaypoints currentWaypoint;
        private float activityInterval; //keep track of how easily the AI can make a move
        private int activityPhase; //keeps track of which phase of the night the AI is in
        private double timeSinceLastAction, timeSinceLastSeen, timeSinceLastRandomAudio;
        private GameObject inCamerasView; //store camera that is viewing AI so it can be scrambled later
        private AIValues nightValues = new AIValues();
        private DynamicWaypoints nextWaypoint;
        private Door door;
        private bool isLookingAtCamera, isWaitingInOffice, possesed;

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
            characterAudioSource.volume = 1;
            characterRandomAudioSource.volume = 1;
            lookAtCurrentCamera = GetComponentInChildren<LookAtCurrentCamera>();
            dummyWaypoint = GameObject.Find("DummyWaypoint").GetComponent<DynamicWaypoints>();
            AwakeExt();
        }

        public virtual void AwakeExt()
        {

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
            timeSinceLastRandomAudio = Time.time;

            SetCharacterState(false);
            StartExt();
        }

        public virtual void StartExt()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //check if able to increase aggression level
            //increase aggressionlevel
            UpdateActivityValues(GameManagerV2.Instance.hour);
            CheckIfBeingWatched();
            PlayRandomAudio();
            if (lookAtCurrentCamera != null)
            {
                lookAtCurrentCamera.lookAtCamera = isLookingAtCamera;
            }

            //Main AI loop
            if (currentWaypoint.isAttackingPosition)
            {
                SetCharacterState(false);

                AttackState();
            }
            //every x seconds
            else if (timeSinceLastAction + activityInterval < Time.time && !currentWaypoint.isAttackingPosition)
            {
                //if roll succeeds
                if (DiceRollGenerator.hasSuccessfulRoll(activityLevel) && !isBeingWatched)
                {
                    if (!possesed)
                    {
                        SetCharacterState(true);
                    }

                    //PlayAudio("default");
                    //decide where to move next
                    nextWaypoint = SetNextWaypoint(currentWaypoint.connectedWaypoints);
                    //move to next waypoint
                    if (nextWaypoint != null)
                    {
                        nextWaypoint.isOccupied = true;
                        Move(nextWaypoint, hasMovementOffset);
                    }
                    else print($"{characterData.characterName} couldn't find a valid waypoint to move to!");
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

        public virtual DynamicWaypoints SetNextWaypoint(List<DynamicWaypoints> potentialWaypoints)
        {
            state = AIState.Searching;
            nextWaypoint = dummyWaypoint;
            int counter = 0;

            for(int i = potentialWaypoints.Count-1; i >= 0; i--)
            {
                if (potentialWaypoints[i].isOccupied)
                {
                    potentialWaypoints.Remove(potentialWaypoints[i]);
                }
            }

            if (potentialWaypoints.Count > 0)
            {
                //bool chooseRandom = true;

                nextWaypoint = potentialWaypoints.GetMaxObject(item => item.flowWeight);

                foreach (var evalWaypoint in potentialWaypoints)
                {
                    //print($"nextwaypoint {nextWaypoint} ({nextWaypoint.flowWeight}) evalWaypoint {evalWaypoint} ({evalWaypoint.flowWeight})");
                    if (evalWaypoint.flowWeight == nextWaypoint.flowWeight)
                    {
                        counter++;
                    }
                }
                if(counter == potentialWaypoints.Count)
                {
                    print($"{characterData.characterName} decided to move to a random waypoint");
                    nextWaypoint = potentialWaypoints[Random.Range(0, potentialWaypoints.Count)];
                }
                else
                {
                    print($"{characterData.characterName} decided to move to a {nextWaypoint} with higher flowWeight {nextWaypoint.flowWeight}");
                }
               
            }
            else
            {
                return null;
            }

            print($"{characterData.characterName} has decided to move to: {nextWaypoint}");

            return nextWaypoint;
        }

        public virtual void Move(DynamicWaypoints wayPoint, bool applyOffset)
        {
            state = AIState.Moving;
            //move to waypoint location
            PlayAudio("movement", false);
            if (applyOffset && !wayPoint.forceNoOffset)
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
            Player.Instance.securityCameraManager.ScrambleCamera();

            if (currentWaypoint.isStartingPoint || currentWaypoint.isCrawling)
            {
                isLookingAtCamera = false;
            }
            else { 
                isLookingAtCamera = DiceRollGenerator.hasSuccessfulRoll(lookChance);
            }
        }

        public virtual void AttackState()
        {
            state = AIState.Attacking;

            if (timeSinceLastAction + nightValues.actionInterval < Time.time)
            {
                if (DiceRollGenerator.hasSuccessfulRoll(activityLevel)) {
                    //AI can decide to move inide the office instead of attacking the player
                    //This will disable the door and prevent it from being closed, this will then resume with the regular AttackState by calling AttackPlayer
                    if (entersOffice && !isWaitingInOffice && !door.isClosed && DiceRollGenerator.hasSuccessfulRoll(enterOfficeChance)) //Maybe only when the player is in cameras?
                    {
                        door.DisableDoor();
                        isWaitingInOffice = true;
                        print($"{characterData.characterName} had entered the office and disabled {door.name}");
                        Move(SetNextWaypoint(currentWaypoint.connectedWaypoints), false);
                    }
                    //TODO: Revisit this logic, will not work as expected now
                    else if (attacksWhenDistracted && Player.Instance.isInCamera || isWaitingInOffice && Player.Instance.isInCamera)
                    {
                        AttackPlayer();
                    }
                    else
                    {
                        AttackPlayer();
                    }
                }
                timeSinceLastAction = Time.time;
            }

        }

        public virtual void AttackPlayer()
        {
            state = AIState.Attacking;
            //attempt to attack the player
            //if in the office dont check for any doors, as they made it passed those already
            if (isWaitingInOffice)
            {
                print($"{characterData.characterName} of type {animatronicType} with level {activityLevel} attacked player from the office");
                Singleton.Instance.SetDeathMessage($"{characterData.characterName} snuck in and attacked you from inside your office.");
                Player.Instance.isBeingAttacked = true;
                if (Player.Instance.isInCamera)
                {
                    Player.Instance.securityCameraManager.ExitSecurityCamera(false);
                }
                StartCoroutine(Jumpscare());
            }
            //if door is closed
            else if (door != null)
            {
                if (door.isClosed)
                {
                    //Play audio and return home
                    AttackFail();
                    Move(homeWaypoint, false);
                    //lower power? 
                }
                else if(!Player.Instance.isBeingAttacked) 
                {
                    print($"{characterData.characterName} of type {animatronicType} with level {activityLevel} attacked player from {door.name} of type {door.animatronicType} with state {door.isClosed}");
                    Singleton.Instance.SetDeathMessage(characterData.characterName, door.name);
                    Player.Instance.isBeingAttacked = true;
                    if (Player.Instance.isInCamera)
                    {
                        Player.Instance.securityCameraManager.ExitSecurityCamera(false);
                    }
                    StartCoroutine(Jumpscare());
                }
            }
            else
            {
                Move(homeWaypoint, false);
                print($"{characterData.characterName} tried to attack the player, but got confused as there was no door!");
            }

            timeSinceLastAction = Time.time;
        }

        public virtual void AttackFail()
        {
            door.BangOnDoor();
            print($"{characterData.characterName} of type {animatronicType} with level {activityLevel} failed to attack player from {door.name} of type {door.animatronicType} with state {door.isClosed}");
        }

        public virtual IEnumerator Jumpscare()
        {
            jumpscareVideo.targetCamera = Camera.main;
            jumpscareVideo.Play();
            yield return new WaitForSeconds((float)jumpscareVideo.clip.length);
            Player.Instance.isAlive = false;
            yield return new WaitForEndOfFrame();

            /* Design #1
             * Door has "jumpscare characters" that will be enabled once a specific character attacks the player.
             * 
             * Design #2 
             * AI will be teleported in front of player, freezing their camera controls 
             * Plays animation after which the player dies.
             * 
             * Design #3
             * AI will move towards the player (how does it move? -> one size fits all animation?)
             * 
             */
        }

        public virtual void SetCharacterState(bool isPossesed)
        {
            if (eyeRenderer != null && possessedEyeMaterial != null && defaultEyeMaterial != null)
            {
                if (isPossesed)
                {
                    UpdateMaterial(eyeRenderer, possessedEyeMaterial);
                    possesed = true;
                }
                else
                {
                    UpdateMaterial(eyeRenderer, defaultEyeMaterial);
                    possesed = false;
                }
            }
        }

        private void UpdateMaterial(SkinnedMeshRenderer mRenderer, Material mat)
        {
            mRenderer.material = mat;
        }

        public virtual void PlayAudio(string audioType, bool isRandom)
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
                    
                    if (isRandom)
                    {
                        characterRandomAudioSource.clip = defaultAudio;
                        characterRandomAudioSource.Play();
                    }
                    else
                    {
                        characterAudioSource.clip = defaultAudio;
                        characterAudioSource.Play();
                    }
                    break;
            }
        }

        public virtual void PlayRandomAudio()
        {if (timeSinceLastRandomAudio + randomAudioInterval < Time.time)
            {
                if (DiceRollGenerator.hasSuccessfulRoll(randomAudioChance))
                {
                    PlayAudio(default, true);
                }
                timeSinceLastRandomAudio = Time.time;
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
            if (other.gameObject.CompareTag("Door")){
                Door newDoor = other.GetComponent<Door>();
                if (door == null)
                {
                    if (newDoor.animatronicType.ToString() == animatronicType.ToString())
                    {
                        door = other.gameObject.GetComponent<Door>();
                    }
                }
            }
        }

        public virtual void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Door"))
            {
                Door newDoor = other.GetComponent<Door>();
                if (door == null)
                {
                    if (newDoor.animatronicType.ToString() == animatronicType.ToString())
                    {
                        door = other.gameObject.GetComponent<Door>();
                    }
                }
            }
        }

        public virtual void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Door"))
            {
                if (door == other.gameObject.GetComponent<Door>())
                {
                    door = null;
                }
            }
        }

        public int GetActivityValue()
        {
            int outputVal = nightValues.activityValues[activityPhase];
            return outputVal;
        }

        public int GetActivityLevel()
        {
            int outputVal = activityLevel;
            return outputVal;
        }

        public string[] GetDebugValues()
        {
            string[] debugValues = new string[8];
            debugValues[0] = characterData.characterName;
            debugValues[1] = activityLevel.ToString();
            float lastAction = (Mathf.Round((Time.time - (float)timeSinceLastAction) * 100f) / 100f); ;

            debugValues[2] = lastAction.ToString();
            debugValues[3] = state.ToString();
            if (currentWaypoint.name != null) debugValues[4] = currentWaypoint.name.ToString();
            else debugValues[4] = "None";
            debugValues[5] = currentWaypoint.connectedWaypoints.Count.ToString();
            debugValues[6] = currentWaypoint.flowWeight.ToString();
            if (door != null) debugValues[7] = door.name.ToString();
            else debugValues[7] = "None";

            return debugValues;
        }

        public Sprite GetCharacterSprite()
        {
            Sprite characterSprite = characterData.thumbnail;
            return characterSprite;
        }
    }
}