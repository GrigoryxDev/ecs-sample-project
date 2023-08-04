using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectedElementUi : MonoBehaviour {
     [SerializeField] private Image icon;
     [SerializeField] private TextMeshProUGUI count;

     public void Init(int startCount, Sprite iconSprite)
     {
        icon.sprite=iconSprite;
       UpdateCount(startCount);
     }

     public void UpdateCount(int newCount)=> count.text=newCount.ToString();
     public void SetActive(bool active)=>gameObject.SetActive(active);
}