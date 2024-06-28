using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace State
{
    public class StateMachine : MonoBehaviour
    {
        // atomic
        private bool _running;

        public State currentState;
        [SerializeField]
        public State initalState;

        /// <summary>
        /// Starts the state machine.
        /// </summary>
        /// <param name="refObj">
        /// The GameObject this state machine is attached to.
        /// </param>
        public void Start()
        {
            currentState = initalState;
            currentState.registerStateMachine(this);
        }

        /// <summary>
        /// Stops the state machine. The currently executing state will finish the update function.
        /// </summary>
        public void Stop()
        {
            Destroy(gameObject.GetComponent(currentState.GetType()));
        }

        public void changeState<nextStateType>() where nextStateType : State
        {
            currentState?.onExitState();
            DestroyImmediate(gameObject.GetComponent(currentState.GetType()));
            gameObject.AddComponent(typeof(nextStateType));
            currentState = gameObject.GetComponent<nextStateType>();
            gameObject.GetComponent<nextStateType>().registerStateMachine(this);

        }
    }
}