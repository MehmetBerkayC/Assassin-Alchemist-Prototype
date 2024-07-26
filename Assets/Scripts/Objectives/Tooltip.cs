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
        if(TryGetComponent(out CraftingItemContainer craftingItem))
        {
            itemNameText.text = craftingItem.ItemData.ItemName;
            itemDescriptionText.text = craftingItem.ItemData.ItemDescription;
            itemStatsText.text = craftingItem.ItemData.ItemStats.ToString();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // do nothing
    }
}
