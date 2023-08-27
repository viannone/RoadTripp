using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro.SpriteAssetUtilities;
using Lean.Transition;

/*
 * Singleton for debugging messages.
 * In a real game, this would be replaced with a more robust system.
 */
namespace RoadTripp
{
    public class BarkSingleton : MonoBehaviour
    {
        private static BarkSingleton instance;
        public TMPro.TextMeshProUGUI text;
        // Start is called before the first frame update
        public static Queue<string> messages = new Queue<string>();
        private static bool CurrentlyDisplayingMessages = false;
        private const string barkSingleton = "BarkSingleton";
        private const float WaitTime = 3.5f;
        private const float FadeTime = 1.0f;
        void Awake()
        {
            //Try to get it on awake, no problem if it's not there
            if (!instance)
            {
                instance = this;
            }
        }

        /*
         * This will get restarted as necessary
         */
        public IEnumerator DisplayMessages()
        {
            CurrentlyDisplayingMessages = true;
            while (messages.Count > 0)
            {
                string messsage = messages.Dequeue();
                text.SetText(messsage);
                text.colorTransition(Color.white, 0.0f).JoinDelayTransition(WaitTime).colorTransition(Color.clear, FadeTime);
                yield return new WaitForSeconds(WaitTime + FadeTime);
            }
            CurrentlyDisplayingMessages = false;
        }

        public static void BarkText(string str)
        {
            if (!instance)//Fetch it if we don't have it.
            {
                Debug.LogWarning("No Bark Singleton");//just a warning, we're still ok

                //The following should only happen once during the entire game
                var gameObject = GameObject.FindGameObjectWithTag(barkSingleton); 
                instance = gameObject.GetComponent<BarkSingleton>();
                if (instance == null) //still null? This shouldn't happen
                {
                    Debug.LogError("Couldn't Find Bark Singleton In Scene");
                    return;
                }
            }
            instance.Bark_Internal(str);
        }

        private void Bark_Internal(string str)
        {
            messages.Enqueue(str);
            if (!CurrentlyDisplayingMessages)
            {
                StartCoroutine(DisplayMessages());
            }
        }
    }

}