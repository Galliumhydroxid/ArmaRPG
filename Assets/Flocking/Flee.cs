using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using State;
using UnityEditor;

public class FleeState : State.State
{

    public VisibilityManager visManager;
    public FlockingBaseComponent flockComp;
    public NavMeshAgent navAgent;
    public const float timeout_s = 0.5f;
    public float flockWeight = 2.0f;
    public float fleeWeight = 1.0f;
    private float timeCounter;
    
    /// <summary>
    /// Is executed as the state is entered.
    /// </summary>
    public override void onEnterState()
    {
        timeCounter = Time.time;
        visManager = GetComponent<VisibilityManager>();
        flockComp = GetComponent<FlockingBaseComponent>();
        navAgent = GetComponent<NavMeshAgent>();
        
    }
    
    public override void onExitState()
    {
        // empty
    }

    /// <summary>
    /// The update loop, linked to the game's frame rate.
    /// </summary>
    public override void update()
    {
        List<GameObject> hunters = visManager.getVisibleObjectsByTag("HUNTER");
        if (hunters.Count() == 0)
        {
            if ((Time.time - timeCounter) > timeout_s)
            {
                stateMachine.changeState<Wander>();
                return;
            }

            navAgent.destination = gameObject.transform.position + flockComp.FlockingVector;
        }
        else
        {
            timeCounter = Time.time;
            Vector3 direction = flockComp.FlockingVector * flockWeight + fleeWeight * getFleeDirection(hunters);
            navAgent.destination = gameObject.transform.position + direction;
        }
    }

    Vector3 getFleeDirection(List<GameObject> objs)
    {
        if (objs.Count() == 0)
        {
            return Vector3.zero;
        }
        Vector3 sum = Vector3.zero;

        foreach (var obj in objs)
        {
            sum += obj.transform.position;
        }

        sum /= objs.Count;
        return gameObject.transform.position - sum;
    }
    
}