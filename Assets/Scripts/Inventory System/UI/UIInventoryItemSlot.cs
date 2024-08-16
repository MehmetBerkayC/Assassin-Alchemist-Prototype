using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class UIInventoryItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemBorderImage;
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemAmountText;

    public event Action<UIInventoryItemSlot> OnItemClicked, OnItemRightClicked, OnItemDrop, OnItemBeginDrag, OnItemEndDrag;

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

    public void OnBeginDrag()
    {
        if (_empty) return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnDrop()
    {
        OnItemDrop?.Invoke(this);
    }
    
    public void OnEndDrag() {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnPointerClick(BaseEventData eventData)
    {
        //if (_empty) return;

        var pointerEventData = eventData as PointerEventData;

        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            OnItemRightClicked?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }
}