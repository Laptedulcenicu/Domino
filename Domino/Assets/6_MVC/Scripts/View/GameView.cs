using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UMLV.MVC
{
    public class GameView : BaseView
    {
        public UnityAction OnFinish;
        public void Finish()
        {
            if (OnFinish != null)
            {
                OnFinish.Invoke();
            }
        }
    }
}
