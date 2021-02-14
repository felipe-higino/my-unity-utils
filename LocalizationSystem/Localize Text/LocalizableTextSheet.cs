using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LocalizationSystemText
{
    internal static class LocalizableTextSheet
    {
        private static TextLocalizationTable TextsTable { get; set; }

        static internal async Task Init()
        {
            var TSVLocations =
                await Addressables.LoadResourceLocationsAsync("Localization", typeof(TextAsset)).Task;

            var first = TSVLocations.FirstOrDefault();
            if (null == first)
                return;

            var TSVAsset =
                await Addressables.LoadAssetAsync<TextAsset>(first).Task;

            if (null == TSVAsset)
                return;

            TextsTable = new TextLocalizationTable(TSVAsset.text);

            var numberOfLanguages = TextsTable.Languages.Count;
            LocalizationSystem.NumberOfLanguages = numberOfLanguages;
        }

        internal static int GetLocalizedTextTagIndex(string tag)
        {
            if (TextsTable == null)
            {
                Debug.LogError("No translation sheet found");
                return -1;
            }
            return TextsTable.Tags.IndexOf(tag);
        }

        internal static string GetLocalizedTextByIndex(int tagIndex)
        {
            if (TextsTable == null)
            {
                Debug.LogError("No translation sheet found");
                return "-NO-TRANSLATION-SHEET-";
            }
            var languageIndex = LocalizationSystem.LanguageIndex;
            return TextsTable.GetTextInMatrix(tagIndex, languageIndex);
        }
    }

}

