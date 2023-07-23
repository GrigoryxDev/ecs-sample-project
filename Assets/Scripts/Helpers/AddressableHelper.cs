using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class AddressableHelper
{
    public static void ReleaseInstance(GameObject gameObject)
    {
        if (!Addressables.ReleaseInstance(gameObject))
        {
            UnityEngine.Object.Destroy(gameObject);
        }
    }

    public static void Release<T>(T asset) => Addressables.Release(asset);

    public static T GetGoAsset<T>(AssetReference reference) where T : Component
    {
        var asyncOpHandle = Addressables.LoadAssetAsync<GameObject>(key: reference);
        var go = asyncOpHandle.WaitForCompletion();
        T result = null;

        if (asyncOpHandle.Status == AsyncOperationStatus.Succeeded)
        {
            result = go.GetComponent<T>();
        }

        return result;
    }

    public static void LoadGOAssetAsyncAndForget<T>(AssetReference reference, Action<T> onLoad) => LoadGOAssetAsync(reference, onLoad).Forget();

    public static async UniTask LoadGOAssetAsync<T>(AssetReference reference, Action<T> onLoad)
    {
        var asyncOpHandle = Addressables.LoadAssetAsync<GameObject>(key: reference);
        var go = await asyncOpHandle;

        if (asyncOpHandle.Status == AsyncOperationStatus.Succeeded)
        {
            var result = go.GetComponent<T>();
            onLoad(result);
        }
    }

    public static T GetAsset<T>(AssetReference reference) where T : UnityEngine.Object
    {
        var asyncOpHandle = Addressables.LoadAssetAsync<T>(key: reference);
        var asset = asyncOpHandle.WaitForCompletion();
        T result = null;
        if (asyncOpHandle.Status == AsyncOperationStatus.Succeeded)
        {
            result = asset;
        }
        return result;
    }

    public static void LoadAsset<T>(AssetReference reference, Action<T> onLoad)
    {
        var asyncOpHandle = Addressables.LoadAssetAsync<T>(key: reference);
        var asset = asyncOpHandle.WaitForCompletion();

        if (asyncOpHandle.Status == AsyncOperationStatus.Succeeded)
        {
            onLoad(asset);
        }
    }

    public static async UniTask LoadAssetAsync<T>(AssetReference reference, Action<T> onLoad)
    {
        var asyncOpHandle = Addressables.LoadAssetAsync<T>(key: reference);
        var asset = await asyncOpHandle;

        if (asyncOpHandle.Status == AsyncOperationStatus.Succeeded)
        {
            onLoad(asset);
        }
    }

    public static void LoadAssetAsyncAndForget<T>(AssetReference reference, Action<T> onLoad) => LoadAssetAsync(reference, onLoad).Forget();
}