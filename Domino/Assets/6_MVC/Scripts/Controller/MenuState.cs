using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UMLV.MVC
{
    public class MenuState : BaseState
    {
        public override void PrepareState(RootController owner)
        {
            base.PrepareState(owner);

            owner.UI.MenuView.OnPlay += PlayGame;
            owner.UI.ShowMenu();
        }

        public override void DestroyState()
        {
            owner.UI.MenuView.OnPlay -= PlayGame;

            base.DestroyState();
        }

        private void PlayGame()
        {
            owner.ChangeState(new GameState());
        }
    }
}
