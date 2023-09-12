using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public static class DiceRollGenerator
    {
        //Rolls dice to determine if they can make a move
        static public bool hasSuccessfulRoll(int aggressionLevel)
        {
            int roll = Random.Range(1, 21);
            //print("roll: " + roll + " aggro: " + aggressionLevel);
            if (aggressionLevel >= roll) //if roll is below aggresionLevel, roll succeeds!
            {
                return true;
            }
            else //roll fails
            {
                return false;
            }
        }
    }
}
