
using System.Collections.Generic;
using System.Linq;
using Core.ECSGameView;
using Core.ECSGameView.Models;
using Core.ECSGameView.Services;
using Core.ECSlogic;
using Core.ECSlogic.Interfaces.Services;
using Core.ECSlogic.Models;
using Core.WorldBuilders;
using Leopotam.EcsLite;
using UniRx;
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

    private EcsWorldModel worldModel;
    private EcsWorld world;

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

    /// <summary>
    /// Should be called only once
    /// </summary>
    public void Init()
    {
        if (IsEcsWorldModelInit())
        {
            return;
        }

        world = new EcsWorld();

        EcsWorldModelInit(world);

        InitLogicWorker();

        InitViewWorker();

        InitCleanupWorker();

        DebugWorker();

        workers.ForEach(w => w.Init(world));


        var initServices = new List<IInitService>
        {
            movablePositionsService,
            initView.GetMapElementsFactory
        };

        foreach (var service in initServices)
        {
            service.Init();
        }

        Observable.EveryUpdate()
        .First(x => initServices.All(x => x.IsInited))
        .Subscribe(_ => worldModel.IsInited = true)
        .AddTo(this);
    }

    private bool IsEcsWorldModelInit()
    {
        return worldModel != null && worldModel.IsInited;
    }

    private void EcsWorldModelInit(EcsWorld world)
    {
        worldModel = new EcsWorldModel(true, world);

        readOnlyGameModel.GetState
        .ObserveEveryValueChanged(x => x.Value)
        .DistinctUntilChanged()
        .Subscribe(newState =>
        {
            if (worldModel != null && newState == GameStates.Game)
            {
                worldModel.IsNeedRestart = true;
            }

        }).AddTo(this);
    }

    private void DebugWorker()
    {
        var debugWorker = new DebugECSWorker();
        workers.Add(debugWorker);
    }

    private void InitCleanupWorker()
    {
        var cleanupWorker = new CleanupECSWorker();
        workers.Add(cleanupWorker);
    }

    private void InitViewWorker()
    {
        var viewWorker = new ViewECSWorker(initView, readOnlyGameModel);
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
        if (!IsEcsWorldModelInit())
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