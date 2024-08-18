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

    [SerializeField] private DraggableItem draggableItem;

    private List<UIInventoryItemSlot> inventorySlots = new();
    private int currentlyDraggedItemIndex = -1;

    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;
    public event Action<int, int> OnSwapItems;

    private void Awake()
    {
        Hide();
        itemDescription.ResetDescription();
        draggableItem.ToggleActive(false);
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

    public void UpdateData(int itemSlotIndex, Sprite itemImage, int itemAmount)
    {
        if(inventorySlots.Count > itemSlotIndex)
        {
            inventorySlots[itemSlotIndex].SetData(itemImage, itemAmount);
        }
    }

    private void HandleShowItemActions(UIInventoryItemSlot slot)
    {
    }
    
    private void HandleBeginDrag(UIInventoryItemSlot slot)
    {
        int slotIndex = inventorySlots.IndexOf(slot);
        if (slotIndex == -1) return;
        currentlyDraggedItemIndex = slotIndex;

        HandleItemSelection(slot);
        OnStartDragging?.Invoke(slotIndex);
    }

    private void HandleEndDrag(UIInventoryItemSlot slot)
    {
        ResetDraggableItem();
    }

    private void HandleDrop(UIInventoryItemSlot slot)
    {
        int slotIndex = inventorySlots.IndexOf(slot);
        if (slotIndex == -1) return;

        OnSwapItems?.Invoke(currentlyDraggedItemIndex, slotIndex);
    }

    public void SetUpDraggedItem(Sprite sprite, int amount)
    {
        draggableItem.ToggleActive(true);
        draggableItem.SetData(sprite, amount);
    }

    private void ResetDraggableItem()
    {
        draggableItem.ToggleActive(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleItemSelection(UIInventoryItemSlot slot)
    {
        int slotIndex = inventorySlots.IndexOf(slot);

        if (slotIndex == -1) return;

        OnDescriptionRequested?.Invoke(slotIndex);
    }

    public void ToggleVisibility()
    {
        if (gameObject.activeInHierarchy) {
            Hide();
        }
        else
        {
            Show();
        }

        //gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggableItem();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        ResetSelection();
    }

    public void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
        foreach (UIInventoryItemSlot item in inventorySlots)
        {
            item.Deselect();
        }
    }

    public void UpdateDescription(int itemIndex, Sprite ýtemSprite, string name, string description)
    {
        itemDescription.SetDescription(ýtemSprite, name, description);
        DeselectAllItems();
        inventorySlots[itemIndex].Select();
    }
}