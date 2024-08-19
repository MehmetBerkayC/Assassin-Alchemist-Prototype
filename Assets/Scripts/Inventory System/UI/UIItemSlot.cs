using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIItemSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField] private Image itemBorderImage;
        [SerializeField] private Image itemSprite;
        [SerializeField] private TextMeshProUGUI itemAmountText;

        public event Action<UIItemSlot> OnItemClicked, OnItemRightClicked, OnItemDrop, OnItemBeginDrag, OnItemEndDrag;

        private bool _empty = true;

        private void Awake()
        {
            ResetData();
            Deselect();
        }

        public void ResetData()
        {
            itemSprite.gameObject.SetActive(false);
            _empty = true;
        }

        public void SetData(Sprite sprite, int amount)
        {
            Debug.Log($"Received: {sprite} {amount}");
            itemSprite.gameObject.SetActive(true);
            itemSprite.sprite = sprite;
            itemAmountText.text = amount.ToString();
            _empty = false;
        }

        public void Deselect()
        {
            itemBorderImage.enabled = false;
        }

        public void Select()
        {
            itemBorderImage.enabled = true;
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDrop?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_empty) return;
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnItemRightClicked?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnDrag(PointerEventData eventData) // Begin and End Drag depends on this
        {
            // Won't use for now
        }
    }
}