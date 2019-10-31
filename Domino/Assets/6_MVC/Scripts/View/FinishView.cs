using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace UMLV.MVC
{
    public class FinishView : BaseView
    {
        [SerializeField]
        private TextMeshProUGUI timeLabel;
        [SerializeField]
        private TextMeshProUGUI scoreLabel;


        public UnityAction OnReplay;
        public void Replay()
        {
            if (OnReplay != null)
            {
                OnReplay.Invoke();
            }
        }

        public UnityAction OnMenu;
        public void Menu()
        {
            if (OnMenu != null)
            {
                OnMenu.Invoke();
            }
        }

        public void ShowResult(GameResult result)
        {
            timeLabel.text = string.Format("{0:00}.{1:000}", (int)result.playedTime, (int)(result.playedTime % 1 * 1000));
            scoreLabel.text = result.score.ToString();
        }
    }
}
