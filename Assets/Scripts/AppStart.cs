using Core.WorldBuilders;
using UnityEngine;
using Zenject;

public class AppStart : MonoBehaviour
{
    private IGameSettingsService gameSettingsService;
    private IEcsEngine ecsEngine;

    [Inject]
    private void Constructor(IGameSettingsService gameSettingsService, IEcsEngine ecsEngine)
    {
        this.gameSettingsService = gameSettingsService;
        this.ecsEngine = ecsEngine;
    }

    private void Start()
    {
        gameSettingsService.ChangeGameSettings(GameSettingsPreset.Default);
        
        //init all services

        //init UI
        ecsEngine.Init();
    }
}
