
using System.Collections.Generic;
using Core.ECSGameView;
using Core.ECSGameView.Models;
using Core.ECSGameView.Services;
using Core.ECSlogic;
using Core.ECSlogic.Interfaces.Services;
using Core.ECSlogic.Models;
using Core.WorldBuilders;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;


public class EcsEngine : MonoBehaviour, IEcsEngine
{
    private IReadOnlyGameModel readOnlyGameModel;
    private IRandomService randomService;
    private IQuestDynamicService questDynamicService;
    private IElementAnalyzeService elementAnalyzeService;
    private IElementStaticService elementStaticService;

    [SerializeField] private MovablePositionsService movablePositionsService;
    [SerializeField] private InitView initView;

    private readonly List<BaseECSWorker> workers = new List<BaseECSWorker>();

    private WorldModel worldModel;


    [Inject]
    private void Constructor(IRandomService randomService,
    IQuestDynamicService questDynamicService, IElementAnalyzeService elementAnalyzeService,
    IElementStaticService elementStaticService, IReadOnlyGameModel readOnlyGameModel)
    {
        this.readOnlyGameModel = readOnlyGameModel;
        this.randomService = randomService;
        this.questDynamicService = questDynamicService;
        this.elementAnalyzeService = elementAnalyzeService;
        this.elementStaticService = elementStaticService;

    }

    public void Init()
    {
        var world = new EcsWorld();
        worldModel = new WorldModel(true, world);

        InitLogicWorker();

        InitViewWorker();

        InitCleanupWorker();

        workers.ForEach(w => w.Init(world));

        worldModel.IsInited = true;
    }

    private void InitCleanupWorker()
    {
        var cleanupWorker = new CleanupECSWorker();
        workers.Add(cleanupWorker);
    }

    private void InitViewWorker()
    {
        var viewWorker = new ViewECSWorker(initView);
        workers.Add(viewWorker);
    }

    private void InitLogicWorker()
    {
        var initLogicServices = new InitLogicServices(randomService,
         questDynamicService, elementAnalyzeService, readOnlyGameModel, movablePositionsService,
         elementStaticService);

        var logicWorker = new LogicECSWorker(worldModel, initLogicServices);
        workers.Add(logicWorker);
    }

    private void Update()
    {
        if (!worldModel.IsInited)
        {
            return;
        }

        workers.ForEach(w => w.Tick());
    }

    public void DestroyWorld()
    {
        workers.ForEach(w => w.Destroy());
    }

    private void OnDestroy()
    {
        DestroyWorld();
    }
}
