using System.Collections;
using System.Collections.Generic;
using Core.ECSlogic.Interfaces.Services;
using UnityEngine;
using Zenject;

public class AppInstaller : MonoInstaller
{
    [SerializeField] private UIHolder uIHolder;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameModel>().AsSingle().NonLazy();


        Container.Bind<IGameSettingsService>().To<GameSettingsService>().AsSingle().NonLazy();

        Container.Bind<IElementStaticService>().To<ElementStaticService>().AsSingle().NonLazy();
        Container.Bind<IElementAnalyzeService>().To<ElementAnalyzeService>().AsSingle().NonLazy();

        Container.Bind<IQuestStorageService>().To<QuestStorageService>().AsSingle().NonLazy();
        Container.Bind<IQuestDynamicService>().To<QuestDynamicService>().AsSingle().NonLazy();

        Container.Bind<IRandomService>().To<RandomService>().AsSingle().NonLazy();


        Container.BindInstance(uIHolder).AsSingle().NonLazy();

        Container.BindInstance(gameObject);
    }
}