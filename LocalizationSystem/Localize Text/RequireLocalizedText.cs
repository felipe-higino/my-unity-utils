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
    private string textLocalizationTag = default;

    [SerializeField, Space(15)]
    private UnityEvent_TranslateText MethodToChangeText = default;

    [ContextMenu("Update this text language")]
    public void UpdateThisTextLanguage()
    {
        var localizedText =
            LocalizableTextSheet.GetLocalizedTextByTag(textLocalizationTag);
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
