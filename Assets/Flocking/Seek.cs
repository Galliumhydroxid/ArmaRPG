using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Flocking
{
    public class Seek : State.State
    {
        public NavMeshAgent mNavMeshAgent;
        public Vector3 latstKnowPosition;
        public float searchTimer;
        private VisibilityManager _visManager;
        public bool reached;
        private string _herdTag;
        // Start is called before the first frame update

        public override void onEnterState()
        {
            
            mNavMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            this.latstKnowPosition = gameObject.GetComponent<HunterParameters>().lastKnownPosition;
            
            this._herdTag = gameObject.GetComponent<HunterParameters>().HerdTag;
            this._visManager = gameObject.GetComponent<VisibilityManager>();
            reached = false;
                
            // go to last know position
            moveTowardsPosition(latstKnowPosition);
        }

        public override void onExitState()
        {
        }

        public override void update()
        {
            //if target is spotted change state to hunt
            Debug.DrawLine(this.transform.position , latstKnowPosition, Color.green);

            List<GameObject> gameObjects = new List<GameObject>();
            var visibleEntities = _visManager.getVisibleObjectsByTag(_herdTag);
            //Debug.Log(visibleEntities.Count);
            //Debug.Log(string.Join(Environment.NewLine, visibleEntities)); 
            if (visibleEntities.Count > 0)
            {
              //  Debug.Log("ep");
                stateMachine.changeState<Hunt>();
            }

            /*
              foreach (var collider in visibleEntities)
              {
                  if (collider.CompareTag("HERD"))
                  {
                      stateMachine.changeState<Hunt>();
                  }
              }
              */
            // Check if we've reached the last known destination


            if (!reached)
            {
                reached = hasTargetBeenReached();
            }
            else
            {
                searchTimer += Time.deltaTime;
                
                moveTowardsPosition(transform.position + (Random.insideUnitSphere * 5 ));
                  
               
                if (searchTimer > 10)
                {
                    stateMachine.changeState<Wander>();
                }
                
            }
            //then search a bit and go back to wander state
            
            
            
        }

        private bool hasTargetBeenReached()
        {
            if (!mNavMeshAgent.pathPending)
            {
                if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
                {
                    if (!mNavMeshAgent.hasPath || mNavMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }
             return false;
        }
        private void moveTowardsPosition(Transform obj)
        {
            moveTowardsPosition(obj.position);
        }
        
        private void moveTowardsPosition(Vector3 obj)
        
        {
            gameObject.GetComponent<NavMeshAgent>().destination = obj;
        }
    }
}