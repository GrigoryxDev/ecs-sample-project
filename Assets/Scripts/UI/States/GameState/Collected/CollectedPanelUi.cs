using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Zenject;

public class CollectedPanelUi : MonoBehaviour
{

    private IReadOnlyGameModel readOnlyGameModel;
    private ISpriteService spriteService;

    [SerializeField] private Transform collectedElementHolder;
    [SerializeField] private CollectedElementUi collectedElement;

    private readonly Dictionary<int, CollectedElementUi> collectedElements = new();

    [Inject]
    private void Constructor(IReadOnlyGameModel readOnlyGameModel,
        ISpriteService spriteService)
    {
        this.readOnlyGameModel = readOnlyGameModel;
        this.spriteService = spriteService;

    }

    public void Init()
    {
        collectedElement.SetActive(false);
        
        readOnlyGameModel.GetCollectedElements.ObserveAdd()
        .Subscribe(x =>
        {
            AddNewCollected(x.Key, x.Value);
        }).AddTo(this);

        readOnlyGameModel.GetCollectedElements.ObserveReplace()
        .Subscribe(x =>
         {
             //x.OldValue mb useful
             UpdateCollected(x.Key, x.NewValue);
         }).AddTo(this);

        readOnlyGameModel.GetCollectedElements.ObserveRemove()
        .Subscribe(x =>
        {
            RemoveCollected(x.Key);
        }).AddTo(this);
    }

    private void RemoveCollected(int id)
    {
        if (collectedElements.TryGetValue(id, out var element))
        {
            element.SetActive(false);
        }
    }

    private void UpdateCollected(int id, int newValue)
    {
        collectedElements[id].UpdateCount(newValue);
    }

    private void AddNewCollected(int id, int startCount)
    {
        CollectedElementUi newCollectedElement;
        if (collectedElements.Count == 0)
        {
            newCollectedElement = collectedElement;
        }
        else
        {
            newCollectedElement = Instantiate(collectedElement, collectedElementHolder);

        }

        spriteService.TryGetSprite(id, out var sprite);
        newCollectedElement.Init(startCount, sprite);
        newCollectedElement.SetActive(true);
        collectedElements.Add(id, newCollectedElement);
    }

}