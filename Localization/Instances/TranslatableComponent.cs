using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
[DisallowMultipleComponent]
public class TranslatableComponent : MonoBehaviour
{
    TMP_Text textComponent = null;
    Unique_LocalizationManager LocalizationManager => Unique<Unique_LocalizationManager>.Get();

    private string _key = "";
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

    private void ActualizeText()
    {
        if (null == LocalizationManager)
            return;

        var textLocalized = LocalizationManager.GetText(_key);
        textComponent.text = textLocalized;
    }
}