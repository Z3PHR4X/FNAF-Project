using System.Collections;
using UnityEngine;
using Zephrax.FNAFGame.Gameplay;

namespace Zephrax.FNAFGame.AI
{
    public class RushEnemyAI : DefaultEnemyAI
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

        [SerializeField] private float attackDuration = 3f;
        private bool isAttacking = false;
        private int attackFailCount;

        public override void AttackState()
        {
            state = AIState.Attacking;
            //hide model
            //somehow show on camera that he is approaching
            if(!isAttacking )
            {
                isAttacking = true;
                StartCoroutine(RunTowardsPlayer(attackDuration));
            }

        }

        public override void CheckIfBeingWatched()
        {
            base.CheckIfBeingWatched();
            isBeingWatched = Player.Instance.isInCamera;
        }

        IEnumerator RunTowardsPlayer(float duration)
        {
            yield return new WaitForSeconds(duration);
            AttackPlayer();
            isAttacking = false;
        }

        public override void AttackFail()
        {
            base.AttackFail();
            int powerDrain = 10 + (attackFailCount * 50);
            Player.Instance.powerManager.powerReserve -= powerDrain;
            attackFailCount++;
            print($"{name} has drained {powerDrain} from the players Power Reserve!");
        }

    }
}
