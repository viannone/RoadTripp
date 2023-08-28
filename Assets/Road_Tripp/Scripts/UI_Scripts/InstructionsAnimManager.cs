using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition;
using UnityEngine.SceneManagement;

namespace RoadTripp
{
    public class InstructionsAnimManager : MonoBehaviour
    {
        public RectTransform[] panels;
        private int currentPanel = 0;
        private float offscreenLoc;
        public float animLength = 1.0f;
        public TMPro.TextMeshProUGUI count;
        public void Awake()
        {
            offscreenLoc = panels[0].rect.width + 100;
            for(int i = 0; i < panels.Length; i++)
            {
                panels[i].transform.localPositionTransition_x(offscreenLoc, 0);
            }
            ShowNewWithAnimDelay();
        }

        private void RemoveOld()
        {
            panels[currentPanel].transform.localPositionTransition_x(offscreenLoc, animLength, LeanEase.Accelerate); //out with the old
        }

        private void ShowNewWithAnimDelay()
        {
            panels[currentPanel].transform.JoinDelayTransition(animLength).localPositionTransition_x(0, animLength, LeanEase.Decelerate);//in with the new
            count?.SetText((currentPanel + 1) + "/" + panels.Length);
        }

        public void Next()
        {
            RemoveOld();
            currentPanel = (currentPanel + 1) % panels.Length;
            ShowNewWithAnimDelay();
        }

        public void Prev()
        {
            RemoveOld();
            currentPanel = (currentPanel - 1 + panels.Length) % panels.Length;
            ShowNewWithAnimDelay();
        }

        public void Exit()
        {
            SceneManager.LoadScene(GameConstants.OpeningScene, LoadSceneMode.Single);
        }
    }
}