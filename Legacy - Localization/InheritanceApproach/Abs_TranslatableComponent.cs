using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
[DisallowMultipleComponent]
public abstract class Abs_TranslatableComponent<EnumLanguages> : MonoBehaviour
    where EnumLanguages : Enum
{
    protected abstract Abs_LocalizationManager<EnumLanguages> LocalizationManager { get; }

    TMP_Text textComponent = null;

    string _key = "";
    /// <summary>
    /// In Awake(), the Id is the GameObject's name. Changing it actualizes the text too;
    /// </summary>
    public string Key
    {
        get => _key;
        set
        {
            if (value == _key)
                return;

            _key = value;
            ActualizeText();
        }
    }

    private void Awake()
    {
        //awake ID
        _key = gameObject.name.Replace("TT - ", "");

        //caching
        textComponent = GetComponent<TMP_Text>();
        //text auto-localized
        if (LocalizationManager)
            LocalizationManager.OnLanguageChange += (_) => ActualizeText();
    }

    private void Start()
    {
        ActualizeText();
    }

    /// <summary>
    /// Helper to actualize texts when necessary, 
    /// not only in OnChangeLanguage event
    /// </summary>
    void ActualizeText()
    {
        if (LocalizationManager)
        {
            var textLocalized = LocalizationManager.GetText(_key);
            textComponent.text = textLocalized;
        }
    }
}