using System;
using UniRx;
using UnityEngine;

public class QuestUiUpdater
{
    private readonly ISpriteService spriteService;
    private readonly IReadOnlyGameModel readOnlyGameModel;
    private readonly QuestUiPanel questUiPanel;
    private IDisposable currentObserve;

    public QuestUiUpdater(ISpriteService spriteService, IReadOnlyGameModel readOnlyGameModel,
    QuestUiPanel questUiPanel)
    {
        this.spriteService = spriteService;
        this.readOnlyGameModel = readOnlyGameModel;
        this.questUiPanel = questUiPanel;
    }

    public void StartObserve()
    {
        readOnlyGameModel.GetCurrentQuest
        .ObserveEveryValueChanged(x => x.Value)
        .DistinctUntilChanged()
        .Subscribe(baseModel =>
        {
            questUiPanel.SetActive(baseModel != null);

            currentObserve?.Dispose();
            if (baseModel != null)
            {
                switch (baseModel.Type)
                {
                    case QuestType.Collect:
                        StartCollectElementsObserve(baseModel);
                        break;
                    default:
                        break;
                }
            }

        }).AddTo(questUiPanel);
    }

    private void StartCollectElementsObserve(QuestBaseModel baseModel)
    {
        var collectquestType = baseModel as CollectElementQuest;

        currentObserve = collectquestType.GetCurrentCount
           .ObserveEveryValueChanged(x => x.Value)
           .DistinctUntilChanged()
           .Subscribe(newVal =>
           {
               spriteService.TryGetSprite(collectquestType.CollectElementId, out Sprite sprite);
               questUiPanel.Init(collectquestType.GetCurrentCount.Value, collectquestType.NeedCount, sprite);

           }).AddTo(questUiPanel);
    }
}