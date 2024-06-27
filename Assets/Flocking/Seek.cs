using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Flocking
{
    public class Seek : State.State
    {
        public float visRange = 5;
        public float visConeAngle = 45;
        public Transform latstKnowPosition;
        public int searchTimer;
        // Start is called before the first frame update

        public override void onEnterState()
        {
            this.latstKnowPosition = gameObject.GetComponent<AiParameters>().lastKnownPosition;
        }

        public override void onExitState()
        {
        }

        public override void update()
        {
            //if target is spotted change state to hung
            List<GameObject> gameObjects = new List<GameObject>();
            var targets = Physics.OverlapSphere(gameObject.transform.position, visRange);
            foreach (var collider in targets)
            {
                if (collider.CompareTag("HERD"))
                {
                    stateMachine.changeState<Hunt>();
                }
            }// go to last know position
            moveTowardsPosition(latstKnowPosition);
            
            //then search a bit and go back to wander state
        }
        
        private void moveTowardsPosition(Transform obj)
        {
            gameObject.GetComponent<NavMeshAgent>().destination = transform.position;
        }
    }
}