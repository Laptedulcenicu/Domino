using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UMLV.MVC
{
    public class GameState : BaseState
    {
        public override void PrepareState(RootController owner)
        {
            base.PrepareState(owner);

            owner.UI.GameView.OnFinish += FinishGame;
            owner.UI.ShowGame();
        }

        public override void DestroyState()
        {
            owner.UI.GameView.OnFinish -= FinishGame;

            base.DestroyState();
        }

        private void FinishGame()
        {
            owner.Storage.SaveData(DataStorageKeys.GAMERESULT_KEY, GameResult.GetRandomResult());
            owner.ChangeState(new FinishState());
        }
    }
}
