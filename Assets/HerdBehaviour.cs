using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class HerdBehaviour : MonoBehaviour
    {
        public FlockingBaseComponent baseComponent;
        public NavMeshAgent agent;
        private float velMod;
        void Start()
        {
            velMod = Random.Range(0.5f, 1.5f);
        }

        void Update()
        {
            agent.velocity = baseComponent.FlockingVector * velMod;
        }
    }
}