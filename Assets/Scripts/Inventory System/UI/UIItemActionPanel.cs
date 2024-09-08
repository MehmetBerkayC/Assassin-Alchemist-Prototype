using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIItemActionPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject actionButtonPrefab;

        public void AddButton(string name, Action onClickAction)
        {
            GameObject button = Instantiate(actionButtonPrefab, transform);
            button.GetComponent<Button>().onClick.AddListener(() => onClickAction());
            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = name;
        }

        public void Toggle(bool value)
        {
            if(value) RemoveOldButtons();
            gameObject.SetActive(value);
        }

        private void RemoveOldButtons()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
