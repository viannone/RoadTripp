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

namespace RoadTripp
{
    public class SemanticTest : MonoBehaviour
    {

        public ARSemanticSegmentationManager _semanticManager;
        public Camera _camera; //cached ref

        private void Start()
        {
            //housekeeping
            if(_semanticManager == null || _camera == null)
            {
                Debug.LogError("Semantic Test not set up properly.");
                return;
            }
            //TODO: Add gameplay elements with semantic buffer
           // _semanticManager.SemanticBufferUpdated += OnSemanticBufferUpdated;
        }

        //cleanup
        private void OnDestroy()
        {
            //_semanticManager.SemanticBufferUpdated -= OnSemanticBufferUpdated;
        }


        void Update()
        {
            var touches = PlatformAgnosticInput.touchCount;
            if (touches <= 0)
            {
                return;
            }

            var touch = PlatformAgnosticInput.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                string[] channelsInTouch = _semanticManager.SemanticBufferProcessor.GetChannelNamesAt((int)touch.position.x, (int)touch.position.y);
                foreach(var s in channelsInTouch)
                {
                    BarkSingleton.BarkText("Touch Registered: " + s);
                }
            }
        }
    }
}