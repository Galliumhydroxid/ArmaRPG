using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class HerdInitializer : MonoBehaviour
    {
        void Start()
        {
            GetComponent<NavMeshAgent>().speed = Random.Range(2.0f, 3.5f);
        }
    }
}