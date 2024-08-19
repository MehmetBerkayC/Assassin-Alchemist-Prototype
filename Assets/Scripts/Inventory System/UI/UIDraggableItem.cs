using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    public class UIDraggableItem : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private UIItemSlot itemSlot;

        private void Awake()
        {
            canvas = transform.root.GetComponent<Canvas>();
            itemSlot = GetComponentInChildren<UIItemSlot>();
        }

        public void SetData(Sprite sprite, int amount)
        {
            itemSlot.SetData(sprite, amount);
        }

        private void Update()
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform, Input.mousePosition,
                canvas.worldCamera, out position);
            transform.position = canvas.transform.TransformPoint(position);
        }

        public void ToggleActive(bool value)
        {
            if (value) Update();

            Debug.Log($"Draggable item active: {value}");
            gameObject.SetActive(value);
        }
    }
}