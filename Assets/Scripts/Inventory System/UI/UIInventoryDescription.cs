using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryDescription : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;

    private void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        itemSprite.gameObject.SetActive(false);
        title.text = "";
        description.text = "";
    }

    public void SetDescription(Sprite itemSprite, string itemName, string itemDescription)
    {
        this.itemSprite.gameObject.SetActive(true);
        this.itemSprite.sprite = itemSprite;
        title.text = itemName;
        description.text = itemDescription;
    }
}
