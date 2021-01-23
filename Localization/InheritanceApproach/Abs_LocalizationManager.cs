using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public abstract class Abs_LocalizationManager<EnumLanguages> : MonoBehaviour
    where EnumLanguages : Enum
{
    [Header("CSV with localizations")]
    [SerializeField] TextAsset LocalizationCSV = null;

    List<List<string>> TranslationMatrix = new List<List<string>>();

    public event Action<EnumLanguages> OnLanguageChange;

    EnumLanguages _actualLanguage = default;
    /// <summary>
    /// Change it to invoke an event that is read for every translatable string
    /// </summary>
    public EnumLanguages ActualLanguage
    {
        get => _actualLanguage;
        set
        {
            if (value.Equals(_actualLanguage))
                return;
            _actualLanguage = value;
            OnLanguageChange?.Invoke(value);
        }
    }

    // private void Awake()
    // {
    //     //unique
    //     Unique<Unique_P_Abs_LocalizationManager<Languages>>.Generate(this);
    //     //persistence
    //     DontDestroyOnLoad(gameObject);
    //     GenerateMatrix();
    // }

    //not very performant to a large matrix
    public string GetText(string ID)
    {
        for (int i = 0; i < TranslationMatrix.Count; i++)
        {
            var currentKey = TranslationMatrix[i][0];
            if (ID == currentKey)
            {
                int n = (int)(IConvertible)ActualLanguage;
                return TranslationMatrix[i][n];
            }
        }
        return "-MISSING-TEXT-";
    }

    protected void GenerateMatrix()
    {
        var matrix = new List<List<string>>();
        var text = LocalizationCSV.text;

        string[] rows = text.Split('\n');
        foreach (var row in rows)
        {
            //generationg row
            var newRow = new List<string>();
            matrix.Add(newRow);

            //generating columns
            string[] columns = row.Split('/');
            foreach (var column in columns)
                newRow.Add(column);
        }
        TranslationMatrix = matrix;
    }
}
