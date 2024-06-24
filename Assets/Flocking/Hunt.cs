using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Flocking
{
    public class Hunt : State.State
    {
        public float visRange = 5;
        public float visConeAngle = 45;

        public override void onEnterState()
        {

        }

        public override void onExitState()
        {

        }

        public override void update()
        {
            // TODO: find nearest npcs
            var nextNPC = nextNPCVisible();
            moveTowardsNPC(nextNPC);
        }

        GameObject nextNPCVisible()
        {
            List<GameObject> gameObjects = new List<GameObject>();
            var targets = Physics.OverlapSphere(gameObject.transform.position, visRange);
            foreach (var collider in targets)
            {
                if (collider.CompareTag("HERD"))
                {
                    gameObjects.Append(collider.gameObject);
                }
            }

            GameObject nearestCandidate = null;
            
            foreach (GameObject obj in gameObjects)
            {
                float angle = Vector3.Angle(obj.transform.forward, gameObject.transform.forward);
                if (angle < visConeAngle)
                {
                    nearestCandidate = closer(obj, nearestCandidate);
                }
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
            // TODO
        }

        private void moveTowardsNPC(GameObject obj)
        {
            gameObject.GetComponent<NavMeshAgent>().destination = obj.transform.position;
        }
    }
}
