#if UNITY_EDITOR
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

    private void OnGUI()
    {
        if (null == configAsset)
            LoadConfigAsset();

        if (null == configAssetEditor && null != configAsset)
            configAssetEditor = Editor.CreateEditor(configAsset);

        //create asset GUI
        if (null == configAsset)
        {
            EditorGUILayout.LabelField("config asset not found");
            if (GUILayout.Button("Create config asset"))
            {
                CreateConfigAsset();
                LoadConfigAsset();
            }
        }
        //scriptable object GUI
        else
        {
            EditorGUILayout.LabelField("asset screen");
            if (Application.isPlaying)
            {
                if (GUILayout.Button("Next Language"))
                    LocalizationSystem.LanguageIndex++;
                if (GUILayout.Button("Previous Language"))
                    LocalizationSystem.LanguageIndex--;
            }
            else
            {
                configAssetEditor?.OnInspectorGUI();

                var validLocalizationAsset =
                    null != configAsset.LocalizationTextAsset.editorAsset;
                if (validLocalizationAsset)
                    if (GUILayout.Button("Download new TSV"))
                        DownloadAndOverrideText();
            }
        }
    }

    private static void CreateConfigAsset()
    {
        var instance = ScriptableObject.CreateInstance<SO_LocalizationConfig>();
        AssetDatabase.CreateAsset(instance, $"Assets/Resources/{configAssetName}.asset");
    }

    private static void LoadConfigAsset()
    {
        configAsset = Resources.Load<SO_LocalizationConfig>(configAssetName);
    }

    private static async void DownloadAndOverrideText()
    {
        var tsv = await configAsset.DownloadTSV();
        var textAssetGUID = configAsset.LocalizationTextAsset.AssetGUID;
        var path = AssetDatabase.GUIDToAssetPath(textAssetGUID);
        File.WriteAllText(path, tsv);
        AssetDatabase.Refresh();
        Debug.Log("Finished to write file!");
    }
}

#endif