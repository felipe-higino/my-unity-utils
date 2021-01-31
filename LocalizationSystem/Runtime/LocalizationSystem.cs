using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AddressableAssets;

public static class LocalizationSystem
{
    private static TextAsset TSVAsset = default;
    public static TranslationSheet sheet = new TranslationSheet();

    [RuntimeInitializeOnLoadMethod]
    private async static void Init()
    {
        TSVAsset = await Addressables.LoadAssetAsync<TextAsset>("Localization").Task;

        if (null == TSVAsset)
        {
            Debug.LogError("Error finding localization text asset");
            return;
        }
        sheet.FromTSV(TSVAsset.text);
    }
}
