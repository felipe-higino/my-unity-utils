using System;
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
        return TSV;
    }
}