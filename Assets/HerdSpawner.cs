using UnityEngine;

namespace DefaultNamespace
{
    public class HerdSpawner : MonoBehaviour
    {
        public GameObject obj;
        public int count;
        public float radius;
        void Start()
        {
            Debug.Log("Spawning Herd");
            for (int i = 0; i < count; i++)
            {
                Instantiate(obj, randomPosAroundFixture(gameObject.transform.position, radius), randomQuat());
            }
        }

        Quaternion randomQuat()
        {
            Vector3 lookVec = Vector3.zero;
            lookVec.x = Random.Range(-1.0f, 1.0f);
            lookVec.z = Random.Range(-1.0f, 1.0f);
            return Quaternion.LookRotation(lookVec);
        }

        Vector3 randomPosAroundFixture(Vector3 fixture, float range)
        {
            Vector3 res;
            res.x = fixture.x + Random.Range(-range, range);
            res.y = 1.0f;
            res.z = fixture.z + Random.Range(-range, range);
            return res;
        }
    }
}