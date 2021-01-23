using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FelipeUtils.Localization.Component;

public class LocalizationManager : MonoBehaviour, ILocalizationManager<Languages>
{
    [SerializeField] Languages defaultLanguage = default;
    [SerializeField] TextAsset localizationTSV = default;

    public Languages ActualLanguage { get; private set; }
    public TextAsset LocalizationTSV { get; private set; }
    public List<List<string>> TranslationMatrix { get; private set; }

    private void Awake()
    {
        Unique<LocalizationManager>.Generate(this);
        ActualLanguage = defaultLanguage;
        LocalizationTSV = localizationTSV;
        TranslationMatrix = this.GenerateMatrix();
    }
}
