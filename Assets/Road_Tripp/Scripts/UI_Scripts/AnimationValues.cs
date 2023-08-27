using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoadTripp
{
    public class AnimationValues
    {
        //static, not const, so user can change in UI preferences screen
        public static float DefaultAnimLength = 2.0f; 
        public static Lean.Transition.LeanEase defaultEasing = Lean.Transition.LeanEase.BounceOut;
    }
}