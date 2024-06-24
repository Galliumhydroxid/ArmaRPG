using JetBrains.Annotations;
using UnityEngine;

namespace State
{
    public abstract class State : MonoBehaviour
    {
        public StateMachine stateMachine;

        public void Start()
        {
            onEnterState();
        }

        public void Update()
        {
            update();
        }

        /// <summary>
        /// Is executed as the state is entered.
        /// </summary>
        public abstract void onEnterState();

        /// <summary>
        /// Is executed as the state is exited.
        /// </summary>
        public abstract void onExitState();

        /// <summary>
        /// The update loop, linked to the game's frame rate.
        /// </summary>
        public abstract void update();

        public void registerStateMachine(StateMachine parent)
        {
            Debug.Log("Registering State Machine");
            this.stateMachine = parent;
        }
    }
}