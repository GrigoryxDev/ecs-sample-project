using System.Collections;
using System.Collections.Generic;
using UniRx;

public interface IReadOnlyGameModel
{
    public IReadOnlyReactiveProperty<GameStates> GetState { get; }
    public IReadOnlyReactiveProperty<QuestBaseModel> GetCurrentQuest { get; }

    public IReadOnlyReactiveDictionary<int, int> GetCollectedElements { get; }

}

public class GameModel : IReadOnlyGameModel
{
    public readonly ReactiveProperty<GameStates> State = new();
    public readonly ReactiveProperty<QuestBaseModel> CurrentQuest = new();

    public readonly ReactiveDictionary<int, int> CollectedElements = new();


    public IReadOnlyReactiveProperty<GameStates> GetState => State;
    public IReadOnlyReactiveProperty<QuestBaseModel> GetCurrentQuest => CurrentQuest;

    public IReadOnlyReactiveDictionary<int, int> GetCollectedElements => CollectedElements;
}