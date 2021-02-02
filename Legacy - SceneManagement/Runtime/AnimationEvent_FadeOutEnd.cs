using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AnimationEvents
{
    public class AnimationEvent_FadeOutEnd : MonoBehaviour
    {
        public static event Action Action_OnFadeOutEnd;
        public void OnFadeOutEnd()
        {
            Action_OnFadeOutEnd?.Invoke();
        }
    }
}

