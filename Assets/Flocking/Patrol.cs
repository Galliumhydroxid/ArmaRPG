using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Flocking
{
    public class Patrol : State.State
    {
        private float startedTime;
        private VisibilityManager visManager;
        private FlockingBaseComponent flockingBaseComponent;
        public float moveTimer;
        private NavMeshAgent mNavMeshAgent;
        private VisibilityManager _visManager;
        private string _herdTag;
        public override void onEnterState()
        {
            mNavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            this._visManager = gameObject.GetComponent<VisibilityManager>();
            this._herdTag = gameObject.GetComponent<HunterParameters>().HerdTag;
        }

        public override void onExitState()
        {
            
        }

        public override void update()
        {
            
           var visibleEntities = _visManager.getVisibleObjectsByTag(_herdTag);
            //Debug.Log(visibleEntities.Count);
            //Debug.Log(string.Join(Environment.NewLine, visibleEntities)); 
            if (visibleEntities.Count > 0)
            {
                stateMachine.changeState<Hunt>();
            }

            
                moveTimer += Time.deltaTime;
                if (moveTimer > Random.Range(3, 5))
                {
                    moveTowardsPosition(transform.position + (Random.insideUnitSphere * 5));
                    moveTimer = 0;
                

            }
        }

        private void moveTowardsPosition(Vector3 insideUnitSphere)
        {
            mNavMeshAgent.destination = insideUnitSphere;
        }
    }
}