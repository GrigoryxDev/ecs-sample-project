using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUiPanel : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI counter;

    public void Init(int startCount, int needCount, Sprite iconSprite)
    {
        icon.sprite = iconSprite;
        UpdateCounter(startCount, needCount);
    }

    public void UpdateCounter(int currentCount, int needCount)
    {
        counter.text = $"Quest: {currentCount}/{needCount}";
    }

    public void SetActive(bool active) => gameObject.SetActive(active);
}