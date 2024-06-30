using System.Collections;
using UnityEngine;
using Zephrax.FNAFGame.Gameplay;

namespace Zephrax.FNAFGame.AI
{
    //Foxy AI Variant, needs to be kept at bay by watching through camera's, advances certain stages before attacking the player
    public class RushEnemyAI : DefaultEnemyAI
    {
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
