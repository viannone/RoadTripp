using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RoadTripp
{
    public class RT_InteractableObject : MonoBehaviour
    {
        public UnityEvent RT_OnClickEvent;

        public void OnClickBehavior()
        {
            RT_OnClickEvent?.Invoke();
        }
    }
}
