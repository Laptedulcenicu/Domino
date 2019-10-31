using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UMLV.MVC
{
    public class BaseState
    {
        protected RootController owner;

        public virtual void PrepareState(RootController owner)
        {
            this.owner = owner;
        }

        public virtual void UpdateState()
        {

        }

        public virtual void DestroyState()
        {

        }
    }
}
