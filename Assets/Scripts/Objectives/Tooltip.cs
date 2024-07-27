using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemStatsText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(TryGetComponent(out UIInventoryItemSlot itemSlot))
        {
            itemNameText.text = itemSlot.InventoryItem.ItemData.ItemName;
            itemDescriptionText.text = itemSlot.InventoryItem.ItemData.ItemDescription;
            itemStatsText.text = itemSlot.InventoryItem.ItemData.ItemStats.ToString();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // do nothing
    }
}
