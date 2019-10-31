using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UMLV.MVC
{
    public class GameResult : Object
    {
        public float playedTime;
        public int score;

        public static GameResult GetRandomResult()
        {
            var result = new GameResult();
            result.playedTime = Random.Range(10.5f, 24.5f);
            result.score = Random.Range(100, 500);
            return result;
        }
    }
}
