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

    // Test
    [Header("Testing")]
    [SerializeField] bool testing = false;

    public Sprite image, image2;
    public int amount;
    public string title, description;
    // ---
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

    private void HandleShowItemActions(UIInventoryItemSlot slot)
    {
    }

    private void HandleEndDrag(UIInventoryItemSlot slot)
    {
    }

    private void HandleDrop(UIInventoryItemSlot slot)
    {
        int slotIndex = inventorySlots.IndexOf(slot);
        if (slotIndex == -1) // empty space
        {
            draggableItem.ToggleActive(false);
            currentlyDraggedItemIndex = -1;
            return;
        }

        inventorySlots[currentlyDraggedItemIndex].SetData(slotIndex == 0 ? image : image2, amount);
        inventorySlots[slotIndex].SetData(currentlyDraggedItemIndex == 0 ? image : image2, amount);
        draggableItem.ToggleActive(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleBeginDrag(UIInventoryItemSlot slot)
    {
        int slotIndex = inventorySlots.IndexOf(slot);
        if (slotIndex == -1) return;
        currentlyDraggedItemIndex = slotIndex;

        draggableItem.ToggleActive(true);
        if(testing) draggableItem.SetData(slotIndex == 0 ? image: image2, amount);
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
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        if (testing)
        {
            inventorySlots[0].SetData(image, amount);
            inventorySlots[1].SetData(image2, amount);
        }
    }
}