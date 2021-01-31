using System;
using System.Collections.Generic;
using UnityEngine;

public class Line
{
    public string[] lines = default;
}

public class TranslationSheet
{
    private List<Line> ListOfLines = new List<Line>();

    public void FromTSV(string tsvString)
    {
        var sheet = new List<Line>();
        string[] lines = tsvString.Split('\n');

        sheet.Clear();
        foreach (var line in lines)
        {
            var items = line.Split('\t');
            var newLine = new Line { lines = items };
            sheet.Add(newLine);
        }
    }
}