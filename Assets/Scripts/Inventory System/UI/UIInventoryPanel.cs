using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject inventoryItemSlotPrefab;

    [SerializeField] private RectTransform inventoryContentPanel;

    private List<UIInventoryItemSlot> inventorySlots = new();

    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryItemSlot itemSlot = Instantiate(inventoryItemSlotPrefab, inventoryContentPanel).GetComponent<UIInventoryItemSlot>();
            inventorySlots.Add(itemSlot);
            itemSlot.OnItemClicked += HandleItemSelection;
            itemSlot.OnItemBeginDrag += HandleBeginDrag;
            itemSlot.OnItemDrop += HandleDrop;
            itemSlot.OnItemEndDrag += HandleEndDrag;
            itemSlot.OnItemRightClicked += HandleShowItemActions;
        }
    }

    private void HandleShowItemActions(UIInventoryItemSlot slot)
    {
    }

    private void HandleEndDrag(UIInventoryItemSlot slot)
    {
    }

    private void HandleDrop(UIInventoryItemSlot slot)
    {
    }

    private void HandleBeginDrag(UIInventoryItemSlot slot)
    {
    }

    private void HandleItemSelection(UIInventoryItemSlot slot)
    {
        Debug.Log(slot.name);
    }

    public void ToggleVisibility()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}