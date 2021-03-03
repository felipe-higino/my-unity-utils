using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Unique_LocalizationManager : MonoBehaviour
{
    [Header("Texts sheets")]
    [SerializeField] TextAsset LocalizationCSV = null;

    public event Action<Languages> OnLanguageChange;

    public Languages ActualLanguage { get; private set; }

    private List<List<string>> TranslationMatrix = new List<List<string>>();

    public void ChangeLanguage(Languages newLanguage)
    {
        if (newLanguage == ActualLanguage)
            return;
        ActualLanguage = newLanguage;
        OnLanguageChange?.Invoke(newLanguage);
    }

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

    private void Awake()
    {
        Unique<Unique_LocalizationManager>.Generate(this);
        GenerateMatrix();
    }

    private void Start()
    {
        __Portugues();
    }

    private void GenerateMatrix()
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
            string[] columns = row.Split('§');
            foreach (var column in columns)
                newRow.Add(column);
        }
        TranslationMatrix = matrix;
    }

    [ContextMenu("Change To English")]
    private void __English()
    {
        ChangeLanguage(Languages.ENG);
    }

    [ContextMenu("Mudar para Português")]
    private void __Portugues()
    {
        ChangeLanguage(Languages.PT_BR);
    }

}