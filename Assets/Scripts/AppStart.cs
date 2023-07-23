using UnityEngine;
using Zenject;

public class AppStart : MonoBehaviour
{
    private IGameSettingsService gameSettingsService;

    [Inject]
    private void Constructor(IGameSettingsService gameSettingsService)
    {
        this.gameSettingsService = gameSettingsService;
    }

    private void Start()
    {
        gameSettingsService.ChangeGameSettings(GameSettingsPreset.Default);
    }
}
