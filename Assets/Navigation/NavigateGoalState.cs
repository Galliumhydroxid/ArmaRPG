using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using State;
using UnityEditor;

public class NavigateGoalState : State.State
{
    private List<Transform> goals;
    private NavMeshAgent agent;
    
    private const float epsilon = 3;
    
    /// <summary>
    /// Is executed as the state is entered.
    /// </summary>
    public override void onEnterState()
    {
        this.goals = gameObject.GetComponent<NavigateGoalParameters>().goals;
        this.agent = gameObject.GetComponent<NavigateGoalParameters>().agent;
        //Debug.Log("Entering state, setting navigation goal");
        agent.destination = goals[0].position; 
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
        Vector3 diff = this.goals[0].position - this.gameObject.transform.position;
        float distToGoal = diff.magnitude;
        if (distToGoal < epsilon)
        {
            //Debug.Log("Distance threshold reached: switching state");
            // set goal to current pos
            agent.destination = agent.transform.position;
            changeState();
        }
    }

    private void changeState()
    {
        gameObject.GetComponent<NavigateGoalParameters>().rotateGoals();
        stateMachine.changeState<NavigateGoalState>();
        //stateMachine.Stop();
    }
}