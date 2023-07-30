using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollGenerator : MonoBehaviour
{
    private int diceSize = 20;

    //Rolls dice to determine if they can make a move
    public bool hasSuccessfulRoll(int aggressionLevel) 
    {
        int roll = Random.Range(0, diceSize+1);
        
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
