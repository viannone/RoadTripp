using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RoadTripp.Debugging;

namespace RoadTripp
{
    public class RT_InteractableObject : MonoBehaviour
    {
        public UnityEvent RT_OnClickEvent;
        public string OptionalDebugMessage;
        public void OnClickBehavior()
        {
            if(!string.IsNullOrEmpty(OptionalDebugMessage))
            {
                BarkSingleton.BarkText(OptionalDebugMessage);
            }
            RT_OnClickEvent?.Invoke();
        }

        public void AddTime(float time)
        {
            PlayerDataManager.AddTime(time);
            BarkSingleton.BarkText("Time Added: " + time);
        }
    }
}
