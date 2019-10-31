using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UMLV.MVC
{
    public class RootController : MonoBehaviour
    {
        private BaseState currentState;

        [SerializeField]
        private DataStorage storage;
        public DataStorage Storage { get { return storage; } }

        [SerializeField]
        private RootUI ui;
        public RootUI UI { get { return ui; } }

        private void Awake()
        {
            ChangeState(new MenuState());
        }

        private void Update()
        {
            if (currentState != null)
            {
                currentState.UpdateState();
            }
        }
        public void ChangeState(BaseState newState)
        {
            if (currentState != null)
            {
                currentState.DestroyState();
            }

            currentState = newState;

            if (currentState != null)
            {
                currentState.PrepareState(this);
            }
        }
    }
}
