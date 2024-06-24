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

        private State _currentState;
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
            _currentState = initalState;
            _currentState.registerStateMachine(this);
        }

        /// <summary>
        /// Stops the state machine. The currently executing state will finish the update function.
        /// </summary>
        public void Stop()
        {
            Destroy(gameObject.GetComponent(_currentState.GetType()));
        }

        public void changeState<nextStateType>() where nextStateType : State
        {
            _currentState?.onExitState();
            DestroyImmediate(gameObject.GetComponent(_currentState.GetType()));
            gameObject.AddComponent(typeof(nextStateType));
            _currentState = gameObject.GetComponent<nextStateType>();
            gameObject.GetComponent<nextStateType>().registerStateMachine(this);

        }
    }
}