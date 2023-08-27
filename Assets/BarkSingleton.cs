using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro.SpriteAssetUtilities;
using Lean.Transition;

/*
 * Singleton for debugging messages
 */
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
        //Try to get it on awake
        if (!instance)
        {
            instance = this;
        }
    }

    //TODO: move this to a coroutine

    public IEnumerator DisplayMessages()
    {
        CurrentlyDisplayingMessages = true;
        while(messages.Count > 0)
        {
            string messsage = messages.Dequeue();
            text.SetText(messsage);
            text.colorTransition(Color.white, 0.0f).JoinDelayTransition(WaitTime).colorTransition(Color.clear, FadeTime);
            yield return new WaitForSeconds(WaitTime + FadeTime);
        }
        CurrentlyDisplayingMessages = false;
    }

    private void Bark_Internal(string str)
    {
        messages.Enqueue(str);
        if(!CurrentlyDisplayingMessages)
        {
            StartCoroutine(DisplayMessages());
        }
    }

    public static void Bark(string str)
    {
        if(!instance)//Fetch it if we don't have it.
        {
            Debug.LogWarning("No Bark Singleton");
            var gameObject = GameObject.FindGameObjectWithTag(barkSingleton);
            instance = gameObject.GetComponent<BarkSingleton>();
            if(instance == null) //still null?
            {
                Debug.LogError("Couldn't Find Bark Singleton In Scene");
                return;
            }
        }
        instance.Bark_Internal(str);
    }

}
