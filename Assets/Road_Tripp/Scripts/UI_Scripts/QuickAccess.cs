using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoadTripp
{
    public class QuickAccess : MonoBehaviour
    {

        public void LoadOpeningScene()
        {
            SceneManager.LoadScene(GameConstants.OpeningScene, LoadSceneMode.Single); //Single is fine for our purposes - no need for additive just yet
        }

        public void LoadPlayScene()
        {
            SceneManager.LoadScene(GameConstants.PlaySceneName, LoadSceneMode.Single);
        }

        public void LoadInstructionsScene()
        {
            SceneManager.LoadScene(GameConstants.InstructionsSceneName, LoadSceneMode.Single);
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