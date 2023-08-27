using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RoadTripp
{
    public class TitleScreenManager : MonoBehaviour
    {
        public RectTransform titlePanel;
        public RectTransform buttonsPanel;

        public void Start()
        {
            //housekeeping
            if(titlePanel == null || buttonsPanel == null)
            {
                Debug.LogError("Fields not set in Title Screen Manager.");
                return;
            }

            /*
             * This functionality can be abstracted to a function if we end up using it in more than just these two lines
             */

            titlePanel.transform.localPositionTransition_y(500, 0)//set title panel outside of screen
                .JoinTransition().localPositionTransition_y(0, AnimationValues.DefaultAnimLength, AnimationValues.defaultEasing);//and smoothly add to screen


            buttonsPanel.transform.localPositionTransition_y(-500, 0)//set buttons panel outside of screen (from the bottom)
                .JoinTransition().localPositionTransition_y(0, AnimationValues.DefaultAnimLength, AnimationValues.defaultEasing);//and smoothly add to screen
        }
     
        
        //Accessed from OnClick() button, set in inspector
        public void PlayGame()
        {
            SceneManager.LoadScene( GameConstants.PlaySceneName, LoadSceneMode.Single); //Single is fine for our purposes
        }

        public void MapScreen()
        {
            SceneManager.LoadScene(GameConstants.MapSceneName, LoadSceneMode.Single);
        }
    }
}
