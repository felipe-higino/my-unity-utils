using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace LocalizationSystemText
{
    internal class Line
    {
        internal string[] lines = default;
    }

    internal class TextLocalizationTable
    {
        internal List<string> Languages { get; } = new List<string>();
        internal List<string> Tags { get; } = new List<string>();

        private List<Line> ListOfLines = new List<Line>();

        internal TextLocalizationTable(string tsvString)
        {
            var lines = tsvString.Split('\n');

            ListOfLines.Clear();
            foreach (var line in lines)
            {
                var items = line.Split('\t');
                var newLine = new Line { lines = items };

                Tags.Add(items.FirstOrDefault());
                ListOfLines.Add(newLine);
            }
            Tags.RemoveAt(0);

            Languages = lines
                .FirstOrDefault()
                .Split('\t')
                .ToList();
            Languages.RemoveAt(0);
        }

        internal string GetTextInMatrix(int tagIndex, int langIndex)
        {
            if (langIndex == -1)
            {
                Debug.LogError("Lang error");
                return "-LANG-ERROR-";
            }

            if (tagIndex == -1)
            {
                Debug.LogError("Tag error");
                return "-TAG-ERROR-";
            }

            var line = ListOfLines.ElementAtOrDefault(tagIndex + 1);
            var text = line?.lines.ElementAtOrDefault(langIndex + 1);

            if (text == "")
            {
                Debug.LogError("text is empty");
                return "-MISSING-TEXT-";
            }

            return text;
        }
    }

}