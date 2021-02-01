using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Line
{
    public string[] lines = default;
}

public class TranslationSheet
{
    public List<string> Languages { get; private set; }
    public List<string> Tags { get; private set; }

    private List<Line> ListOfLines = new List<Line>();

    public TranslationSheet(string tsvString)
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

    public string GetText(string tag, string language)
    {
        var languageIndex = Languages.IndexOf(language);
        if (languageIndex == -1)
        {
            Debug.LogError("Lang error");
            return "-LANG-ERROR-";
        }
        var tagIndex = Tags.IndexOf(tag);
        if (languageIndex == -1)
        {
            Debug.LogError("Tag error");
            return "-TAG-ERROR-";
        }

        var line = ListOfLines.ElementAtOrDefault(tagIndex + 1);
        var text = line?.lines.ElementAtOrDefault(languageIndex + 1);
        if (text == "")
            return "-MISSING-TEXT-";

        return text;
    }
}