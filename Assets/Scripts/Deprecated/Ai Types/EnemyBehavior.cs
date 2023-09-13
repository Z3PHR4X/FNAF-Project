using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public bool isDeprecated = true;
    public GameManager gameManager;
    public NavMeshAgent agent;
    public AudioSource charAudio;
    public bool isFreddy;
    public int[] levelProgression;
    [SerializeField] private int[] aggressionSetup = {0,0,0,0,0,0,0};
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private bool isGoingLeft;
    [SerializeField] private float actionInterval;
    [SerializeField] private GameObject head;
    [SerializeField] private TextMeshPro debugText;
    
    private int AiLevel;
    private float lastActionTime;
    private float levelupTime;
    private static float levelupInterval;
    private static int aggressiveness;
    private int levelupPhase;
    private int phase;
    private Collider[] cameraBuffer = new Collider[1];
    private int maskId;
    
    //Need to look at this
    //https://imgur.com/a/xe01I#aaR227u

    // Start is called before the first frame update
    void Start()
    {
        maskId = LayerMask.GetMask("PostProcessing");
        gameManager = FindObjectOfType<GameManager>();
        levelupInterval = GameManager.hourLength;
        phase = 0;
        levelupPhase = 0;
        aggressiveness = aggressionSetup[Singleton.Instance.selectedNight-1];
        AiLevel = aggressiveness;
        lastActionTime = Time.time+actionInterval;
        levelupTime = Time.time;
        Move(phase);
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseLevel();
        ActionRoutine();
        if (phase != 0) {
        LookAtCamera();
        }
        Debug();
    }

    private void ActionRoutine()
    {
        if (!IsMoving())
        {
            if (IsReadyToMove())
            {                
                if(isActionValid())
                {
                    MakeSound();
                    phase++; 
                    //print(agent.name + "'s current phase: " + phase);
                    if (phase > waypoints.Length-1)
                    { 
                        Attack();
                    }
                    else
                    {
                        Move(phase);
                    }
                }
            }
        }
        else
        {
            lastActionTime = Time.time;
        }
    }

    private void IncreaseLevel() //increases the ai level at each hour
    {
        if (levelupTime + levelupInterval < Time.time && levelupPhase < levelProgression.Length - 1)
        {
            levelupTime = Time.time;
            //print(agent.name + "'s AI Level increased from: " + AiLevel + " to " + (AiLevel + levelProgression[levelupPhase]));
            AiLevel += levelProgression[levelupPhase];
            levelupPhase++;
        }
        else
        {
            return;
        }
    }

    private bool IsMoving() //checks whether the agent is still moving
    {
        if (agent.velocity != Vector3.zero)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private bool IsReadyToMove() //Keeps track of time between moves
    {
        //print("time to beat: " + (movementTimer + movementTime) + " current time: " + Time.time);
        if (lastActionTime + actionInterval < Time.time)
        {
            //print(agent.name + " is ready to make a move!");
            lastActionTime = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool isActionValid() //Rolls 20 sided dice to determine if they can make a move
    {
        int roll = Random.Range(1, 21);
        //print("Roll is " + roll);
        if (AiLevel >= roll)
        {
            //print(agent.name + "'s roll succeeded! Current AI Level: " + AiLevel);
            return true;
        }
        else
        {
            //print(agent.name + "'s roll failed! Current AI Level: " + AiLevel);
            return false;
        }
    }

    private void MakeSound()
    {
        if (!charAudio.isPlaying)
        {
            if (isFreddy)
            {
                charAudio.Play();
            }
            else if (Random.Range(0, 21) < 6)
            {
                charAudio.Play();
            }
        }
    }
    
    private void Attack() //Attacks the player
    {
        if (isGoingLeft)
        {
            if (!gameManager._isLeftDoorClosed)
            {
                KillPlayer();
            }
            else
            {
                //print(agent.name + "Tried to kill the player but the left door was closed!");
                ResetPosition();
            }
        }
        else
        {
            if (!gameManager._isRightDoorClosed)
            {
                KillPlayer();
            }
            else
            {
                //print(agent.name + "Tried to kill the player but the right door was closed!");
                ResetPosition();
            }
        }
        
    }
    
    private void Move(int inputPhase) //Moves the animatronic around
    {
        agent.SetDestination(waypoints[inputPhase].transform.position);
        //print(agent.name + " is moving to " + waypoints[inputPhase]);
    }

    private void ResetPosition() //Return to starting position
    {
        //print(agent.name + " has been reset!");
        phase = 0;
        Move(phase);
    }

    private void KillPlayer() //kill
    {
        JumpScare();
        //print(agent.name + " killed the player!");
        gameManager.isPlayerAlive = false;
    }

    private void JumpScare() //PLAY SPOOPY ANIMATION
    {
        //play jumpscare! 
        //wait till finished and then continue
    }

    private void LookAtCamera()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, 10f, cameraBuffer, maskId);
        if (count > 0)
        {
            head.transform.LookAt(cameraBuffer[0].transform.position, Vector3.left);
        }
        else
        {
            //head.transform.rotation = Quaternion.Lerp(head.transform.rotation, new Quaternion(0,0,0, 0), Time.time * 0.01f);
            head.transform.rotation = new Quaternion(0,0,0,0);
        }
        
    }

    private void Debug()
    {
        debugText.text = name +"\n Level: " + AiLevel;
    }
}
