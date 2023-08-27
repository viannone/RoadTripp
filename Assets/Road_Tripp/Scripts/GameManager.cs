using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.ARDK.Utilities;
using Niantic.ARDK.Utilities.Input.Legacy;
using Niantic.ARDK.Extensions;
using Niantic.ARDK.Networking.MultipeerNetworkingEventArgs;
using Niantic.ARDK.Utilities.BinarySerialization;
using System.IO;
using System.Linq;

namespace RoadTripp
{
    public class GameManager : MonoBehaviour
    {
        public ARNetworkingManager manager;
        public Camera camera;

        public TMPro.TextMeshProUGUI text;

        public const uint TEST = 0;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(TryConnect(10, 1.0f));
        }

        //This should be accessible from a menu
        public IEnumerator TryConnect(int attempts, float interval)
        {
            yield return null;
            //we have to wait for the networking object to be instantiated before we append our code to the callback
            //we don't control that
            for(int i = 0; i < attempts; i++)
            {
                var networking = manager?.NetworkSessionManager?.Networking;
                if(networking != null)
                {
                    Debug.Log("ARDK Networking Ready.");
                    BarkSingleton.BarkText("ARDK Networking Ready");
                    networking.Connected += OnNetworkInitialized;
                    break;
                }
                else
                {
                    BarkSingleton.BarkText("Attempt " + attempts + " to Connect to Network Object. Unsuccessful.");
                    Debug.LogFormat("Attempt {attempts} to Connect to Network Object. Unsuccessful.", i);
                    yield return new WaitForSeconds(interval);
                }
            }
        }

        private void Update()
        {
            if(PlatformAgnosticInput.touchCount <= 0)
            {
                return;
            }
            var touch = PlatformAgnosticInput.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                var currentFrame = manager.ARSessionManager.ARSession.CurrentFrame;
                if (currentFrame == null)
                {
                    Debug.LogError("No Current Frame");
                    BarkSingleton.BarkText("No Current Frame");
                    return;
                }
                var hitTestResults = currentFrame.HitTest(
                    camera.pixelWidth, camera.pixelHeight,
                    touch.position, Niantic.ARDK.AR.HitTest.ARHitTestResultType.EstimatedHorizontalPlane);
                if(hitTestResults == null || hitTestResults.Count == 0)
                {
                    return;
                }
                var position = hitTestResults[0].WorldTransform.GetPosition();
                //TESTING
                var stream = new MemoryStream();
                GlobalSerializer.Serialize(stream, System.DateTime.Now.Second.ToString());
                BroadCast(stream.ToArray());
            }
        }

        void OnNetworkInitialized(ConnectedArgs args)
        {
            manager.NetworkSessionManager.Networking.PeerDataReceived += PeerDataRecieved;
        }

        //Data Coming In
        void PeerDataRecieved(PeerDataReceivedArgs args)
        {
            if(args.Tag == TEST)
            {
                var stream = new MemoryStream(args.CopyData());
                var str = "Click at seconds: " + (string)GlobalSerializer.Deserialize(stream);
                Debug.Log(str);
                BarkSingleton.BarkText(str);
                text.SetText(str);
            }
        }

        //Data going out (including to us)
        void BroadCast(byte[] data)
        {
            Debug.Log(manager.NetworkSessionManager.Networking.OtherPeers.Count);
            manager.ARNetworking.Networking.SendDataToPeers(TEST, data, manager.NetworkSessionManager.Networking.OtherPeers, Niantic.ARDK.Networking.TransportType.ReliableUnordered, true);
        }
    }
}