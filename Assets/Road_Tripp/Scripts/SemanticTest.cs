using Niantic.ARDK.AR.Awareness.Semantics;
using Niantic.ARDK.Extensions;
using Niantic.ARDK.Utilities.Input.Legacy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RoadTripp.Debugging;

using Niantic.ARDK.AR.ARSessionEventArgs;
using Niantic.ARDK.AR.Awareness;
using Niantic.ARDK.AR.Awareness.Semantics;
using System.Linq;
using System;

namespace RoadTripp
{
    public class SemanticTest : MonoBehaviour
    {
        private float remainingTime;
        public ARSemanticSegmentationManager _semanticManager;
        public Camera _camera; //cached ref
        public string _listenToChannel = "flower_experimental";
        public TMPro.TextMeshProUGUI clock;
        private void Start()
        {
            //housekeeping
            if(_semanticManager == null || _camera == null)
            {
                Debug.LogError("Semantic Test not set up properly.");
                return;
            }
            remainingTime = PlayerDataManager.CurrentTimeBank;
            //TODO: Add gameplay elements with semantic buffer
           // _semanticManager.SemanticBufferUpdated += OnSemanticBufferUpdated;
        }

        void Update()
        {
            remainingTime -= Time.deltaTime;
            clock.text = TimeSpan.FromSeconds(remainingTime).ToString(@"mm\:ss");
            if (remainingTime <= 0)
            {
                PlayerDataManager.Lose();
                return;
            }
            var touches = PlatformAgnosticInput.touchCount;
            if (touches <= 0)
            {
                return;
            }

            var touch = PlatformAgnosticInput.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                string[] channelsInTouch = _semanticManager.SemanticBufferProcessor.GetChannelNamesAt((int)touch.position.x, (int)touch.position.y);
                if (channelsInTouch.Length == 0)
                {
                    BarkSingleton.BarkText("Touch Registered, but nothing detected");
                }
                else if(channelsInTouch.Contains(_listenToChannel))
                {
                    PlayerDataManager.Win();   
                }
                else
                {
                    BarkSingleton.BarkText("Touch Registered: " + string.Join(" ,", channelsInTouch));
                }
            }
        }
    }
}