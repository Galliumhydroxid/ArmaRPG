using System.Collections;
using System.Collections.Generic;
using State;
using UnityEngine;
using UnityEngine.AI;

public class Wander : State.State
{
    public Vector3 navigationTarget;
    public float distMod = 20;
    public float timeout_s = 5;
    private float startedTime;
    private VisibilityManager visManager;
    public float epsilon = 5f;
    public float fleePercentageThreshold = 0.2f;
    
    public override void onEnterState()
    {
        navigationTarget = gameObject.transform.position + Random.insideUnitSphere * distMod;
        GetComponent<NavMeshAgent>().destination = navigationTarget;
        startedTime = Time.time;
        visManager = GetComponent<VisibilityManager>();
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

        float dist = (gameObject.transform.position - navigationTarget).magnitude;

        if (dist < epsilon)
        {
            nextState();
            return;
        }
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
