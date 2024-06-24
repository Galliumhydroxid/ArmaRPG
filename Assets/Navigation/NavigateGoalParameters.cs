using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigateGoalParameters : MonoBehaviour
{
    public List<Transform> goals;
    public float epsilon = 3;
    public NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rotateGoals()
    {
        var first = goals[0];
        goals.RemoveAt(0);
        goals.Add(first);
    }
}
