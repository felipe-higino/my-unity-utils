using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class LocalizationSystem
{
    private static TextAsset TSVAsset = default;
    private static TranslationSheet Sheet { get; set; }

    private static string languageName = "";
    public static string LanguageName
    {
        get => languageName;
        set
        {
            if (!Sheet.Languages.Contains(value))
            {
                Debug.LogError("Invalid language");
                return;
            }

            languageName = value;
            languageIndex = Sheet.Languages.IndexOf(value);

            UpdateAllTexts();
        }
    }

    private static int languageIndex = 0;
    public static int LanguageIndex
    {
        get => languageIndex;
        set
        {
            var index = value;
            var numberOfLanguages = Sheet.Languages.Count();
            if (value < 0)
                index = numberOfLanguages - 1;
            if (value > numberOfLanguages - 1)
                index = 0;

            languageIndex = index;
            languageName = Sheet.Languages.ElementAtOrDefault(index);

            UpdateAllTexts();
        }
    }

    /// <summary>
    /// Forces all <see cref="RequireLocalizedText"/> to be updated
    /// </summary>
    public static void UpdateAllTexts()
    {
        foreach (var localizable in RequireLocalizedText.AllLocalizableTexts)
        {
            localizable.RequireThisComponentUpdate();
        }
    }

    internal static string GetLocalizedTextWithTag(string tag)
    {
        if (Sheet == null)
        {
            Debug.LogError("No translation sheet found");
            return "-NO-TRANSLATION-SHEET-";
        }
        return Sheet.GetText(tag, languageName);
    }

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
        languageName = Sheet.Languages?.FirstOrDefault();

        var provisoryObject = new GameObject("[My Localization]");
        provisoryObject.AddComponent<ProvisoryObject>();
    }

    private class ProvisoryObject : MonoBehaviour
    {
        // call start from static class :) 
        private void Start()
        {
            LocalizationSystem.UpdateAllTexts();
            Destroy(this.gameObject);
        }
    }

}
