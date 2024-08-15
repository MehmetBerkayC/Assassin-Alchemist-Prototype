using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIInventoryPanel inventoryPanel;

    [Header("Settings")]
    [SerializeField] private KeyCode inventoryToggleKey = KeyCode.I;

    public int InventorySize = 10;

    private void Start()
    {
        inventoryPanel.InitializeInventoryUI(InventorySize);    
    }

    private void Update()
    {
        ToggleInventory();
    }

    private void ToggleInventory()
    {
        if (Input.GetKeyDown(inventoryToggleKey))
        {
            inventoryPanel.ToggleVisibility();
        }
    }
}
