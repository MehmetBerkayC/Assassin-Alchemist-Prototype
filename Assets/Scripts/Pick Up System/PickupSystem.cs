using Inventory;
using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PickupSystem : MonoBehaviour
{
    [Header("References")]
    // might want to change naming later to include everything with an inventory
    [SerializeField]
    private InventoryController playerInventory; 

    [SerializeField]
    private InventorySO inventoryData;

    [Header("Settings")]
    [SerializeField] private float detectionRadius = 2f;
    [SerializeField] private LayerMask pickupableMask;

    private Collider2D[] collisions;

    private void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryController>();
        if (playerInventory != null )
        {
            inventoryData = playerInventory.GetInventoryData();
        }
    }

    private void Update()
    {
        DetectInteractableItems();
    }

    private void DetectInteractableItems()
    {
        collisions = Physics2D.OverlapCircleAll(transform.position, detectionRadius, pickupableMask);
        if (collisions.Length > 0)
        {
            foreach (Collider2D col in collisions)
            {
                IPickupable pickupableObject;
                if(col.gameObject.TryGetComponent(out pickupableObject))
                {
                    //Debug.Log($"Pickupable object detected: {pickupableObject}");
                    PickupItem(pickupableObject);
                }
            }
        }
    }

    private void PickupItem(IPickupable pickupableObject)
    {
        Item itemToPickup = pickupableObject.Pickup();
        if (itemToPickup != null)
        {
            //Debug.Log($"Got item {itemToPickup}");  
            int remainder = inventoryData.AddItem(itemToPickup.InventoryItem, itemToPickup.Amount);
            //Debug.Log($"Amount left {remainder}");
            if (remainder == 0)
            {
                itemToPickup.DestroyItem();
            }
            else
            {
                itemToPickup.Amount = remainder;
            }
        }
    }
}
