using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class LocalizationSystem
{
    private static TextAsset TSVAsset = default;
    public static TranslationSheet Sheet { get; private set; }

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
}
