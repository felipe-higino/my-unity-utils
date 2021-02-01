using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UnityEngine.AddressableAssets
{
    [Serializable]
    public class AssetReferenceText : AssetReferenceT<TextAsset>
    {
        public AssetReferenceText(string guid) : base(guid)
        {
        }
    }
}

public class SO_LocalizationConfig : ScriptableObject
{
    [SerializeField, ReadOnly]
    private List<string> languageTags = default;
    public List<string> LanguageTags => languageTags;

    [SerializeField]
    private AssetReferenceText localizationTextAsset = default;
    public AssetReferenceText LocalizationTextAsset => localizationTextAsset;

    [SerializeField]
    private string docId = "";

    [SerializeField, Min(1)]
    private int sheetNumber = 1;


    private string RemoteUrl =>
        $"https://docs.google.com/spreadsheets/d/{docId}/export?format=tsv&sheet={sheetNumber}";

    [ContextMenu("Download and print tsv")]
    public async Task<string> DownloadTSV()
    {
        Debug.Log("Downloading TSV file ...");
        var client = new HttpClient();
        var bytesResponse = await client.GetByteArrayAsync(RemoteUrl);
        var TSV = System.Text.Encoding.UTF8.GetString(bytesResponse);
        Debug.Log("Download result: \n\n" + TSV);

        var tagsFromTSV = TSV
            .Split('\n')
            .FirstOrDefault()
            .Split('\t')
            .ToList();
        tagsFromTSV.RemoveAt(0);
        languageTags = tagsFromTSV;

        return TSV;
    }
}