using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
internal class UnityEvent_TranslateText : UnityEvent<string> { }

public class RequireLocalizedText : MonoBehaviour
{
    [SerializeField]
    private UnityEvent_TranslateText MethodToChangeText = default;

    [SerializeField]
    private string localizationTag = default;

    [ContextMenu("change text")]
    private void ChangeText()
    {
        var localizedText = LocalizationSystem.GetLocalizedText(localizationTag);
        MethodToChangeText?.Invoke(localizedText);
    }
}
