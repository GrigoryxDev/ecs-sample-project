using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameUIState : BaseState
{
    [SerializeField] private Button plusButton;
    [SerializeField] private Button updateButton;
    [SerializeField] private Button removeButton;
    public override GameStates GetState => GameStates.Game;

    public readonly ReactiveDictionary<int, int> CollectedElements = new();
    public IReadOnlyReactiveDictionary<int, int> GetCollectedElements => CollectedElements;
    private void Start()
    {
        plusButton.onClick.AddListener(() =>
        {
            if (!GetCollectedElements.ContainsKey(0))
            {
                CollectedElements.Add(0, 1);
            }
        });

        updateButton.onClick.AddListener(() =>
        {
            if (CollectedElements.ContainsKey(0))
            {
                CollectedElements[0]++;
            }
        });

        removeButton.onClick.AddListener(() =>
        {
            if (CollectedElements.ContainsKey(0))
            {
                CollectedElements.Remove(0);
            }
        });

        GetCollectedElements.ObserveAdd().Subscribe(x =>
        {
            Debug.Log($"Add Key {x.Key}, Value {x.Value}");
        });

        GetCollectedElements.ObserveReplace().Subscribe(x =>
        {
            Debug.Log($"Replace Key {x.Key}, OldValue {x.OldValue} - NewValue {x.NewValue}");
        });

        GetCollectedElements.ObserveRemove().Subscribe(x =>
        {
            Debug.Log($"Remove Key {x.Key}, Value {x.Value}");
        });
    }
}

