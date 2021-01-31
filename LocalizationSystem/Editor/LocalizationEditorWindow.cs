using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class LocalizationEditorWindow : EditorWindow
{
    private const string configAssetName = "LocalizationConfig";
    private static SO_LocalizationConfig configAsset = null;
    private static Editor configAssetEditor = null;

    [MenuItem("Felipe Utils/Localization")]
    private static void ShowWindow()
    {
        GetWindow<LocalizationEditorWindow>("Localization Window");
        LoadConfigAsset();
    }

    private void OnEnable()
    {
        LoadConfigAsset();
        if (null != configAsset)
            configAssetEditor = Editor.CreateEditor(configAsset);
    }

    private void OnGUI()
    {
        if (null == configAsset)
            ConfigAssetNotFound();
        else
            AssetScreen();
    }

    private void ConfigAssetNotFound()
    {
        EditorGUILayout.LabelField("config asset not found");
        if (GUILayout.Button("Create config asset"))
            CreateConfigAsset();
    }

    private void AssetScreen()
    {
        EditorGUILayout.LabelField("asset screen");
        configAssetEditor?.OnInspectorGUI();

        var validLocalizationAsset = null != configAsset.LocalizationTextAsset.editorAsset;
        if (validLocalizationAsset)
            if (GUILayout.Button("Download new TSV"))
                DownloadAndOverrideText();
    }

    private static void CreateConfigAsset()
    {
        var instance = ScriptableObject.CreateInstance<SO_LocalizationConfig>();
        AssetDatabase.CreateAsset(instance, $"Assets/Resources/{configAssetName}.asset");
        LoadConfigAsset();
    }

    private static void LoadConfigAsset()
    {
        configAsset = Resources.Load<SO_LocalizationConfig>(configAssetName);
    }

    private async void DownloadAndOverrideText()
    {
        var tsv = await configAsset.DownloadTSV();
        var textAssetGUID = configAsset.LocalizationTextAsset.AssetGUID;
        var path = AssetDatabase.GUIDToAssetPath(textAssetGUID);
        File.WriteAllText(path, tsv);
    }
}
