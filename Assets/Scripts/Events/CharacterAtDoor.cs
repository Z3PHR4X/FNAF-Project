using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class CharacterAtDoor : MonoBehaviour
    {
        public GameManager gameManager;
        public bool isLeftDoor;

        private void OnTriggerEnter(Collider other)
        {
            //print(other.name + " has entered");
            if (other.tag == "Enemy")
            {
                if (isLeftDoor)
                {
                    gameManager._isEnemyAtLeftDoor = true;
                }
                else
                {
                    gameManager._isEnemyAtRightDoor = true;
                }
                
            }
        }

        private void OnTriggerStay(Collider other)
        {
            //print(other.name + " has stayed");
            if (other.tag == "Enemy")
            {
                if (isLeftDoor)
                {
                    gameManager._isEnemyAtLeftDoor = true;
                }
                else
                {
                    gameManager._isEnemyAtRightDoor = true;
                }

            }
        }

        private void OnTriggerExit(Collider other)
        {
            //print(other.name + " has left");
            if (other.tag == "Enemy")
            {
                if (isLeftDoor)
                {
                    gameManager._isEnemyAtLeftDoor = false;
                }
                else
                {
                    gameManager._isEnemyAtRightDoor = false;
                }

            }

        }
    }
}
