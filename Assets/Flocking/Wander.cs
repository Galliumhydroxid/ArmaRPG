using System.Collections;
using System.Collections.Generic;
using State;
using UnityEngine;
using UnityEngine.AI;

public class Wander : State.State
{
    public Vector3 navigationTargetRel;
    public float distMod = 0.3f;
    public float timeout_s = 5;
    private float startedTime;
    private VisibilityManager visManager;
    private FlockingBaseComponent flockingBaseComponent;
    private NavMeshAgent agent;
    public float epsilon = 0.1f;
    public float fleePercentageThreshold = 0.4f;
    
    public override void onEnterState()
    {
        navigationTargetRel = Random.insideUnitSphere * distMod;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = gameObject.transform.position + navigationTargetRel;
        startedTime = Time.time;
        visManager = GetComponent<VisibilityManager>();
        flockingBaseComponent = GetComponent<FlockingBaseComponent>();
    }

    public override void onExitState()
    {
    }

    public override void update()
    {
        //timeout?
        if ((Time.time - startedTime) > timeout_s)
        {
            nextState();
            return;
        }

        List<GameObject> hunters = visManager.getVisibleObjectsByTag("HUNTER");
        if (hunters.Count > 0)
        {
            changeToFlee();
            return;
        }

        float ratio = countFleeRatio(visManager.getVisibleObjectsByTag("HERD"));
        if (ratio >= fleePercentageThreshold)
        {
            changeToFlee();
            return;
        }

        float dist = (gameObject.transform.position - agent.destination).magnitude;

        if (dist <= epsilon)
        {
            nextState();
            return;
        }

        adjustNavTarget();
    }


    void adjustNavTarget()
    {
        agent.destination = gameObject.transform.position + navigationTargetRel + flockingBaseComponent.FlockingVector;
    }
    
    
    float countFleeRatio(List<GameObject> objs)
    {
        float fleeCount = 0;

        foreach (var obj in objs)
        {
            if (obj.GetComponent<StateMachine>().currentState.GetType() == typeof(FleeState))
            {
                fleeCount++;
            }
        }

        return fleeCount / objs.Count;
    }
    
    private void nextState()
    {
        stateMachine.changeState<Wander>();
    }

    private void changeToFlee()
    {
        stateMachine.changeState<FleeState>();
    }
}
