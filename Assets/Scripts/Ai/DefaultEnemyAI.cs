using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public AIValues aiValues;
    public bool isBeingWatched;
    
    private int activityLevel; //keep track of how easily the AI can make a move
    private int activityPhase; //keeps track of which phase of the night the AI is in
    private double timeSinceLastAction;
    private GameObject inCamerasView; //store camera that is viewing AI so it can be scrambled later


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //every x seconds
            //if not being watched
                //if roll succeeds
                    //perform action
                        //decide where to move next
                        //move to next waypoint
                        //OR
                        //attempt to attack player
                            //if fails
                                //return to starting position

            //check if able to increase aggression level
                //increase aggressionlevel
    }

    private void Move(GameObject wayPoint)
    {
       //move to waypoint location
       //scramble camera
    }

    private void AttackPlayer()
    {
        //attempt to attack the player
            //if door is closed
                //return to start
                //play banging sound
                //lower power
            //else
                //kill player
    }

    private void ReturnToStart()
    {
        //return to starting position
    }

}
