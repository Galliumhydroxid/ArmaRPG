using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class HerdBehaviour : MonoBehaviour
    {
        public FlockingBaseComponent baseComponent;
        public NavMeshAgent agent;
        void Start()
        {
        }

        void Update()
        {
            Debug.Log(baseComponent.FlockingVector);
            agent.velocity = baseComponent.FlockingVector;
        }
    }
}