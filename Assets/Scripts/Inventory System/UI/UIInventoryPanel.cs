using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIInventoryItemSlot inventoryItemSlotPrefab;
    
    [SerializeField] private UIInventoryDescription itemDescription;

    [SerializeField] private RectTransform inventoryContentPanel;

    private List<UIInventoryItemSlot> inventorySlots = new();

    // Test
    [Header("Testing")]
    [SerializeField] bool testing = false;

    public Sprite image;
    public int amount;
    public string title, description;
    // ---
    private void Awake()
    {
        Hide();
    }

    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryItemSlot itemSlot = Instantiate(inventoryItemSlotPrefab, Vector3.zero, Quaternion.identity, parent: inventoryContentPanel);
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
        if (testing)
        {
            itemDescription.SetDescription(image,title,description);
            inventorySlots[0].Select();
        }

        Debug.Log(slot.name);
    }

    public void ToggleVisibility()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        if (testing) {
            inventorySlots[0].SetData(image, amount);
        }
    }
}