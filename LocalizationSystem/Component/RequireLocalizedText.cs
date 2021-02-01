using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
internal class UnityEvent_TranslateText : UnityEvent<string> { }

public class RequireLocalizedText : MonoBehaviour
{
    internal static List<RequireLocalizedText> AllLocalizableTexts = new List<RequireLocalizedText>();

    [SerializeField]
    private string localizationTag = default;

    [SerializeField, Space(15)]
    private UnityEvent_TranslateText MethodToChangeText = default;

    [ContextMenu("Change text")]
    public void RequireThisComponentUpdate()
    {
        var localizedText = LocalizationSystem.GetLocalizedTextWithTag(localizationTag);
        MethodToChangeText?.Invoke(localizedText);
    }

    private void Awake()
    {
        AllLocalizableTexts.Add(this);
    }

    private void OnDestroy()
    {
        AllLocalizableTexts.Remove(this);
    }

}
