using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Flocking
{
    public class Hunt : State.State
    {
        private VisibilityManager _visManager;
        private string _herdTag;
        [FormerlySerializedAs("latestKnowPosition")] public Vector3 latestKnowPosition;



        public override void onEnterState()
        {
            this._herdTag = gameObject.GetComponent<HunterParameters>().HerdTag;
            this._visManager = gameObject.GetComponent<VisibilityManager>();


        }

        public override void onExitState()
        {
            gameObject.GetComponent<HunterParameters>().lastKnownPosition = this.latestKnowPosition;

        }

        public override void update()
        {   
          //  Debug.Log("ep");
            // TODO: find nearest npcs
            var nextNPC = nextNPCVisible();
            if (!nextNPC)
            {
                changeStateToSearch();
                return;
            }
           // Debug.Log(nextNPC);
            moveTowardsNPC(nextNPC);
            
         
            latestKnowPosition = nextNPC.transform.position;
            Debug.DrawLine(this.transform.position , latestKnowPosition, Color.yellow);


        }

        GameObject nextNPCVisible()
        {
          /*  List<GameObject> gameObjects = new List<GameObject>();
            var targets = Physics.OverlapSphere(gameObject.transform.position, visRange);
            foreach (var collider in targets)
            {
                if (collider.CompareTag("HERD"))
                {
                    gameObjects.Append(collider.gameObject);
                }
            }
        */
          var visibleEntities = _visManager.getVisibleObjectsByTag(_herdTag);

            GameObject nearestCandidate = null;
            
            foreach (GameObject obj in visibleEntities)
            {
                    nearestCandidate = closer(nearestCandidate, obj );
                
            }

            if (nearestCandidate == null)
            {
                changeStateToSearch();
            }

            return nearestCandidate;
        }

        private GameObject closer(GameObject a, GameObject b)
        {
            if (a == null)
            {
                return b;
            }
            
            var diffa = a.transform.position - gameObject.transform.position;
            var diffb = b.transform.position - gameObject.transform.position;
            return diffa.magnitude < diffb.magnitude ? a : b;
        }

        private void changeStateToSearch()
        {
            stateMachine.changeState<Seek>();
        }

        private void moveTowardsNPC(GameObject obj)
        {
            if (obj != null)
            {
                Vector3 flockingVec = GetComponent<FlockingBaseComponent>().FlockingVector;
                gameObject.GetComponent<NavMeshAgent>().destination = obj.transform.position;
            }
        }
    }
}
