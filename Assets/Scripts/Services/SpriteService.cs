using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.ECSGameView.Services;
using Cysharp.Threading.Tasks;

public interface ISpriteService : IInitService
{
    bool TryGetSprite(int id, out Sprite sprite);
}
public class SpriteService : ISpriteService
{
    private readonly IElementStaticService elementStaticService;
    private readonly WorldStorage worldStorage;
    private readonly Dictionary<int, Sprite> spritePool = new();
    public bool IsInited { get; private set; }


    public SpriteService(IElementStaticService elementStaticService, WorldStorage worldStorage)
    {
        this.elementStaticService = elementStaticService;
        this.worldStorage = worldStorage;
    }

    public void Init()
    {
        CreateSpritesAsync().Forget();
    }

    public bool TryGetSprite(int id, out Sprite sprite)
    {
        return spritePool.TryGetValue(id, out sprite);
    }

    private async UniTask CreateSpritesAsync()
    {
        var allElements = elementStaticService.GetAllElementIds();
        var taskList = new List<UniTask>();
        foreach (var item in allElements)
        {
            var reference = worldStorage.ElementsWorldStorage.GetSpriteRef(item);
            taskList.Add(AddressableHelper.LoadAssetAsync<Sprite>(reference, (sprite) =>
            {
                spritePool.Add(item, sprite);
            }));
        }

        await UniTask.WhenAll(taskList);
        IsInited = true;
    }
}
