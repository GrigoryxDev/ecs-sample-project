using System.Collections;
using System.Collections.Generic;
using Core.ECSlogic.Interfaces.Services;
using Core.WorldBuilders;
using UnityEngine;
using Zenject;

public class AppInstaller : MonoInstaller
{
    [SerializeField] private UIHolder uIHolder;
    [SerializeField] private EcsEngine ecsEngine;
    [SerializeField] private WorldStorage worldStorage;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameModel>().AsSingle().NonLazy();


        Container.Bind<IGameSettingsService>().To<GameSettingsService>().AsSingle().NonLazy();

        Container.Bind<IElementStaticService>().To<ElementStaticService>().AsSingle().NonLazy();
        Container.Bind<IElementAnalyzeService>().To<ElementAnalyzeService>().AsSingle().NonLazy();

        Container.Bind<IQuestStorageService>().To<QuestStorageService>().AsSingle().NonLazy();
        Container.Bind<IQuestDynamicService>().To<QuestDynamicService>().AsSingle().NonLazy();

        Container.Bind<IRandomService>().To<RandomService>().AsSingle().NonLazy();

        Container.Bind<IEndGameService>().To<EndGameService>().AsSingle().NonLazy();
        Container.Bind<IRestartService>().To<RestartService>().AsSingle().NonLazy();

        Container.Bind<ISpriteService>().To<SpriteService>().AsSingle().NonLazy();

        Container.Bind<IEcsEngine>().FromInstance(ecsEngine).AsSingle().NonLazy();

        Container.BindInstance(uIHolder).AsSingle().NonLazy();

        Container.BindInstance(worldStorage).AsSingle().NonLazy();

        Container.BindInstance(gameObject).AsSingle().NonLazy();
    }
}