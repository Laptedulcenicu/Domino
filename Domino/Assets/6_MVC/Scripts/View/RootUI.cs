using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UMLV.MVC
{
    public class RootUI : MonoBehaviour
    {
        [SerializeField]
        private MenuView menuView;
        public MenuView MenuView { get { return menuView; } }

        [SerializeField]
        private GameView gameView;
        public GameView GameView { get { return gameView; } }
        [SerializeField]
        private FinishView finishView;
        public FinishView FinishView { get { return finishView; } }

        private void HideAll()
        {
            menuView.HideView();
            gameView.HideView();
            finishView.HideView();
        }

        public void ShowMenu()
        {
            HideAll();
            menuView.ShowView();
        }

        public void ShowGame()
        {
            HideAll();
            gameView.ShowView();
        }

        public void ShowFinish()
        {
            HideAll();
            finishView.ShowView();
        }
    }
}
