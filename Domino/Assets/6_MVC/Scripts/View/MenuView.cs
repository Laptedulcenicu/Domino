using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UMLV.MVC
{
    public class MenuView : BaseView
    {
        public UnityAction OnPlay;
        public void Play()
        {
            if (OnPlay != null)
            {
                OnPlay.Invoke();
            }
        }
    }
}
