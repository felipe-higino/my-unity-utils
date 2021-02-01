using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class LocalizationSystem
{

    private static TextAsset TSVAsset = default;
    private static TranslationSheet Sheet { get; set; }
    private static string ActualLanguage { get; set; } = "en";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private async static void Init()
    {
        TSVAsset = await Addressables.LoadAssetAsync<TextAsset>("Localization").Task;

        if (null == TSVAsset)
        {
            Debug.LogError("Error finding localization text asset");
            return;
        }
        Sheet = new TranslationSheet(TSVAsset.text);
    }

    public static string GetLocalizedText(string tag)
    {
        if (Sheet == null)
            return "-NO-TRANSLATION-SHEET-";
        return Sheet.GetText(tag, ActualLanguage);
    }
}
