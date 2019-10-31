using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UMLV.MVC
{
    public class FinishState : BaseState
    {
        public override void PrepareState(RootController owner)
        {
            base.PrepareState(owner);

            owner.UI.FinishView.OnReplay += ReplayGame;
            owner.UI.FinishView.OnMenu += GoToMenu;

            var data = owner.Storage.GetData<GameResult>(DataStorageKeys.GAMERESULT_KEY);
            owner.UI.FinishView.ShowResult(data);

            owner.UI.ShowFinish();
        }

        public override void DestroyState()
        {
            owner.UI.FinishView.OnReplay -= ReplayGame;
            owner.UI.FinishView.OnMenu -= GoToMenu;

            base.DestroyState();
        }

        private void ReplayGame()
        {
            owner.ChangeState(new GameState());
        }

        private void GoToMenu()
        {
            owner.ChangeState(new MenuState());
        }
    }
}
