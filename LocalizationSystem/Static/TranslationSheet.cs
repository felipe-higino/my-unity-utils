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
    public List<string> Languages { get; } = new List<string>();
    public List<string> Tags { get; } = new List<string>();

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

    public string GetText(string tag, int languageIndex)
    {
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
        {
            Debug.LogError("text is empty");
            return "-MISSING-TEXT-";
        }

        return text;
    }

    // public string GetText_tagname(string tag, string language)
    // {
    //     var languageIndex = Languages.IndexOf(language);
    //     if (languageIndex == -1)
    //     {
    //         Debug.LogError("Lang error");
    //         return "-LANG-ERROR-";
    //     }
    //     var tagIndex = Tags.IndexOf(tag);
    //     if (languageIndex == -1)
    //     {
    //         Debug.LogError("Tag error");
    //         return "-TAG-ERROR-";
    //     }

    //     var line = ListOfLines.ElementAtOrDefault(tagIndex + 1);
    //     var text = line?.lines.ElementAtOrDefault(languageIndex + 1);
    //     if (text == "")
    //     {
    //         Debug.LogError("text is empty");
    //         return "-MISSING-TEXT-";
    //     }

    //     return text;
    // }
}