using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AnimationEvents
{
    public class AnimationEvent_FadeInEnd : MonoBehaviour
    {
        public static event Action Action_OnFadeInEnd;

        public void OnFadeInEnd()
        {
            Action_OnFadeInEnd?.Invoke();
        }
    }
}

