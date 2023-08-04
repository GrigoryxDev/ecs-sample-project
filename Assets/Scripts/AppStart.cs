using System;
using System.Collections.Generic;
using System.Linq;
using Core.ECSGameView.Services;
using Core.WorldBuilders;
using UniRx;
using UnityEngine;
using Zenject;

public class AppStart : MonoBehaviour
{
    private IGameSettingsService gameSettingsService;
    private IEcsEngine ecsEngine;
    private IRestartService restartService;
    private ISpriteService spriteService;
    private UIHolder uIHolder;

    [Inject]
    private void Constructor(IGameSettingsService gameSettingsService, IEcsEngine ecsEngine,
    IRestartService restartService, ISpriteService spriteService, UIHolder uIHolder)
    {
        this.gameSettingsService = gameSettingsService;
        this.ecsEngine = ecsEngine;
        this.restartService = restartService;
        this.spriteService = spriteService;
        this.uIHolder = uIHolder;
    }

    private void Start()
    {
        InitStartServices(AfterServicesInit);
        gameSettingsService.ChangeGameSettings(GameSettingsPreset.Default);
    }

    private void AfterServicesInit()
    {
        uIHolder.Init();
        restartService.Restart();
        ecsEngine.Init();
    }

    private void InitStartServices(Action onInit)
    {
        List<IInitService> services = new List<IInitService>
        {
            spriteService
        };

        foreach (var service in services)
        {
            service.Init();
        }

        Observable.EveryUpdate()
        .First(x => services.All(x => x.IsInited))
        .Subscribe(_ =>
        onInit()).AddTo(this);
    }
}
