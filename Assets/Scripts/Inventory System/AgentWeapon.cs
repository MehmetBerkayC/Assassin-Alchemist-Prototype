using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]
    private EquipableItemSO weapon;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState;

    public void SetWeapon(EquipableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if(weapon != null) // Unequip previous item
        {
            inventoryData.AddItem(weapon, 1, itemCurrentState);
        }

        weapon = weaponItemSO;
        itemCurrentState = new List<ItemParameter>(itemState);
        ModifyParameters();
    }

    private void ModifyParameters()
    {
        foreach(ItemParameter itemParameter in parametersToModify)
        {
            if (itemCurrentState.Contains(itemParameter))
            {
                int index = itemCurrentState.IndexOf(itemParameter);
                float newValue = itemCurrentState[index].Value + itemParameter.Value;
                itemCurrentState[index] = new ItemParameter
                {
                    Parameter = itemParameter.Parameter,
                    Value = newValue
                };
            }
        }
    }
}
