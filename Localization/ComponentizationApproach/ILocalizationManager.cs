using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FelipeUtils.Localization.Component
{
    public interface ILocalizationManager<T> where T : Enum
    {
        T ActualLanguage { get; }
        TextAsset LocalizationTSV { get; }
        List<List<string>> TranslationMatrix { get; }
    }

    public static class ILocalizationManagerExtensions
    {
        static public string GetText<T>(this ILocalizationManager<T> manager, string ID) where T : Enum
        {
            var TranslationMatrix = manager.TranslationMatrix;
            var ActualLanguage = manager.ActualLanguage;
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

        static public List<List<string>> GenerateMatrix<T>(this ILocalizationManager<T> manager) where T : Enum
        {
            var matrix = new List<List<string>>();
            var text = manager.LocalizationTSV.text;

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
            return matrix;
        }
    }
}
