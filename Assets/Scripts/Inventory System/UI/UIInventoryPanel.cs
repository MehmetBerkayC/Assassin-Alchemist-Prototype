using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventoryPanel : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private UIItemSlot inventoryItemSlotPrefab;
    
        [SerializeField] private UIInventoryDescription itemDescription;

        [SerializeField] private RectTransform inventoryContentPanel;

        [SerializeField] private UIDraggableItem draggableItem;

        [SerializeField] private UIItemActionPanel actionPanel;

        private List<UIItemSlot> inventorySlots = new();
        private int currentlyDraggedItemIndex = -1;

        public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;
        public event Action<int, int> OnSwapItems;

        private void Awake()
        {
            Hide();
        }

        private void Start()
        {
            itemDescription.ResetDescription();
            draggableItem.ToggleActive(false);
        }

        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIItemSlot itemSlot = Instantiate(inventoryItemSlotPrefab, Vector3.zero, Quaternion.identity, parent: inventoryContentPanel);
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

        private void HandleShowItemActions(UIItemSlot slot)
        {
            int slotIndex = inventorySlots.IndexOf(slot);
            if (slotIndex == -1) return;
            OnItemActionRequested?.Invoke(slotIndex);
        }
    
        private void HandleBeginDrag(UIItemSlot slot)
        {
            int slotIndex = inventorySlots.IndexOf(slot);
            if (slotIndex == -1) return;
            currentlyDraggedItemIndex = slotIndex;

            HandleItemSelection(slot);
            OnStartDragging?.Invoke(slotIndex);
        }

        private void HandleEndDrag(UIItemSlot slot)
        {
            ResetDraggableItem();
        }

        private void HandleDrop(UIItemSlot slot)
        {
            // item swapping -> empty slots also get swapped (same logic)
            int slotIndex = inventorySlots.IndexOf(slot);
            if (slotIndex == -1) return;

            OnSwapItems?.Invoke(currentlyDraggedItemIndex, slotIndex);
            HandleItemSelection(slot);
        }

        public void SetUpDraggableItem(Sprite sprite, int amount)
        {
            draggableItem.ToggleActive(true);
            draggableItem.SetData(sprite, amount);
        }

        private void ResetDraggableItem()
        {
            draggableItem.ToggleActive(false);
            currentlyDraggedItemIndex = -1;
        }

        private void HandleItemSelection(UIItemSlot slot)
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
            actionPanel.Toggle(false);
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
            foreach (UIItemSlot item in inventorySlots)
            {
                item.Deselect();
            }
            actionPanel.Toggle(false);
        }

        public void ShowItemAction(int itemIndex)
        {
            actionPanel.Toggle(true);
            actionPanel.transform.position = inventorySlots[itemIndex].transform.position;
        }

        public void AddAction(string actionName, Action performAction)
        {
            actionPanel.AddButton(actionName, performAction);
        }

        public void UpdateDescription(int itemIndex, Sprite ýtemSprite, string name, string description)
        {
            itemDescription.SetDescription(ýtemSprite, name, description);
            DeselectAllItems();
            inventorySlots[itemIndex].Select();
        }

        public void ResetAllItems()
        {
            foreach (var item in inventorySlots)
            {
                item.ResetData();
                item.Deselect();
            }
        }
    }
}