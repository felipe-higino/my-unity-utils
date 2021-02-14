using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using LocalizationSystemAudio;
using LocalizationSystemText;

public static class LocalizationSystem
{
    internal static int NumberOfLanguages = 0;

    private static int languageIndex = 0;
    public static int LanguageIndex
    {
        get => languageIndex;
        set
        {
            var index = value;
            if (value < 0)
                index = NumberOfLanguages - 1;
            if (value > NumberOfLanguages - 1)
                index = 0;

            languageIndex = index;
            UpdateAllTexts();
            UpdateAllAudios();
        }
    }

    /// <summary>Forces all <see cref="RequireLocalizedAudio"/> to be updated</summary>
    public static void UpdateAllAudios()
    {
        foreach (var localizableAudio in RequireLocalizedAudio.AllLocalizableAudio)
        {
            localizableAudio.UpdateThisAudioLanguage();
        }
    }

    /// <summary>Forces all <see cref="RequireLocalizedText"/> to be updated</summary>
    public static void UpdateAllTexts()
    {
        foreach (var localizableText in RequireLocalizedText.AllLocalizableTexts)
        {
            localizableText.UpdateThisTextLanguage();
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private async static void Init()
    {
        var initTextSheet = LocalizableTextSheet.Init();
        var initAudioDatabase = SO_LocalizableAudioDatabase.Init();

        await Task.WhenAll(initTextSheet, initAudioDatabase);

        var provisoryObject = new GameObject("[My Localization]");
        provisoryObject.AddComponent<ProvisoryObject>();
    }

    private class ProvisoryObject : MonoBehaviour
    {
        // call start from static class :) 
        private void Start()
        {
            LocalizationSystem.UpdateAllTexts();
            LocalizationSystem.UpdateAllAudios();
            Destroy(this.gameObject);
        }
    }
}
