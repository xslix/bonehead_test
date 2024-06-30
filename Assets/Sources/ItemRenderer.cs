using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ItemRenderer : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI levelText;
    
    public void SetItem(Item item, ItemsData itemsData)
    {
        var itemInfo = itemsData.itemsInfo[item.id];
        icon.sprite = itemInfo.sprite;
        background.color = itemsData.rarityInfo.FirstOrDefault(x => x.rarity == item.rarity)?.color ?? Color.white;
        levelText.text = item.level.ToString();
    }


}
