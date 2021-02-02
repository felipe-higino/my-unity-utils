using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

internal static class LocalizableTextSheet
{
    private static TextAsset TSVAsset = default;
    private static TextLocalizationTable TextsTable { get; set; }

    static internal async Task Init()
    {
        TSVAsset =
            await Addressables.LoadAssetAsync<TextAsset>("Localization").Task;

        if (null == TSVAsset)
        {
            Debug.LogError("Error finding localization text asset");
            return;
        }

        TextsTable = new TextLocalizationTable(TSVAsset.text);

        var numberOfLanguages = TextsTable.Languages.Count;
        LocalizationSystem.NumberOfLanguages = numberOfLanguages;
    }

    internal static string GetLocalizedTextByTag(string tag)
    {
        if (TextsTable == null)
        {
            Debug.LogError("No translation sheet found");
            return "-NO-TRANSLATION-SHEET-";
        }
        var languageIndex = LocalizationSystem.LanguageIndex;
        return TextsTable.GetText(tag, languageIndex);
    }
}
