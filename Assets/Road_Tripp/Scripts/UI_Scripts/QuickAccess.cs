using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoadTripp
{
    public class QuickAccess : MonoBehaviour
    {
        public void LoadPlayScene()
        {
            SceneManager.LoadScene(GameConstants.PlaySceneName, LoadSceneMode.Single); //Single is fine for our purposes
        }

        public void LoadMapScene()
        {
            SceneManager.LoadScene(GameConstants.MapSceneName, LoadSceneMode.Single);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}