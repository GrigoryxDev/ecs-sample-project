
using UnityEngine;

public interface IGameSettingsService
{
    void ChangeGameSettings(GameSettingsPreset preset);
}

public class GameSettingsService : IGameSettingsService
{
    public void ChangeGameSettings(GameSettingsPreset preset)
    {
        //Any presets could be here
        switch (preset)
        {
            default:
                QualitySettings.vSyncCount = 1;
                Application.targetFrameRate = 60;

                Debug.Log("Default game settings preset");
                break;
        }
    }
}

public enum GameSettingsPreset
{
    Default

}