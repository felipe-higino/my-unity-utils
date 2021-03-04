#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace LocalizationSystemText
{
    public class LocalizationEditorWindow : EditorWindow
    {
        private static SO_TextLocalization configAsset = null;
        private static Editor configAssetEditor = null;

        [MenuItem("Felipe Utils/Text Localization Helper")]
        private static void ShowWindow()
        {
            GetWindow<LocalizationEditorWindow>("Text Localization Helper");
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
                        null != configAsset.LocalizationTextAsset;
                    if (validLocalizationAsset)
                        if (GUILayout.Button("Download new TSV"))
                            DownloadAndOverrideText();
                }
            }
        }

        private static void CreateConfigAsset()
        {
            var instance = ScriptableObject.CreateInstance<SO_TextLocalization>();
            AssetDatabase.CreateAsset(instance, $"Assets/Resources/{DefaultName.name}.asset");
        }

        private static void LoadConfigAsset()
        {
            configAsset = Resources.Load<SO_TextLocalization>(DefaultName.name);
        }

        private static async void DownloadAndOverrideText()
        {
            var tsv = await configAsset.DownloadTSV();
            // var textAssetGUID = configAsset.LocalizationTextAsset.AssetGUID;
            // var path = AssetDatabase.GUIDToAssetPath(textAssetGUID);
            var path = AssetDatabase.GetAssetPath(configAsset.LocalizationTextAsset);
            File.WriteAllText(path, tsv);
            AssetDatabase.Refresh();
            Debug.Log("Finished to write file!");
        }
    }

}


#endif