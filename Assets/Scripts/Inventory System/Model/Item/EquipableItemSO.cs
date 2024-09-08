using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "Inventory System/Items/Equipable Item", fileName = "new ItemSO")]
    public class EquipableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";

        [field: SerializeField]
        public AudioClip ActionSFX { get; private set; }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
            
            if (weaponSystem != null) {
                weaponSystem.SetWeapon(this, itemState == null? DefaultParametersList : itemState);
                return true;
            }
            return false;
        }
    }
}