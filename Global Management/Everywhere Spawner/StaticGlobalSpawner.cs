using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

public static class StaticGlobalSpawner
{
    private const string label = "Everywhere";

    [RuntimeInitializeOnLoadMethod]
    public async static void Init()
    {
        var everywhereAssets =
            await Addressables.LoadResourceLocationsAsync(label).Task;

        foreach (var asset in everywhereAssets)
        {
            if (asset.ResourceType == typeof(GameObject))
                InstantiateAsset(asset);
        }
    }

    private async static void InstantiateAsset(IResourceLocation obj)
    {
        var spawned = await Addressables.InstantiateAsync(obj).Task;
        spawned.name = $"[{obj.PrimaryKey}]";
    }
}
