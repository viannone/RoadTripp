using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoadTripp
{
    public class PlayerDataManager : MonoBehaviour
    {
        public static float CurrentTimeBank { get; private set; }
        public const float minTime = 30;

        public static int SignPostsCollects;
        public static int POIsCollected;

        static PlayerDataManager()
        {
            CurrentTimeBank = minTime;
        }

        public static void AddTime(float time)
        {
            CurrentTimeBank = Mathf.Max(CurrentTimeBank, minTime);// must always start with at least min time
            CurrentTimeBank += time;
        }


        public static void Win()
        {
            SceneManager.LoadScene(GameConstants.VictoryScene, LoadSceneMode.Single);
        }

        public static void Lose()
        {
            SceneManager.LoadScene(GameConstants.MapSceneName, LoadSceneMode.Single);// For now, you can't really lose, just go back to the map
        }
    }
}
