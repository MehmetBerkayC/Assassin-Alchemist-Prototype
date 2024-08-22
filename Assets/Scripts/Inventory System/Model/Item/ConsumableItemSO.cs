using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "Inventory System/Items/Consumable Item", fileName = "new ItemSO")]
    public class ConsumableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField]
        private List<ModifierData> modifiers = new();
        public string ActionName => "Consume";

        public AudioClip ActionSFX { get; private set; }

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            foreach (ModifierData data in modifiers)
            {
                data.StatModifier.AffectCharacter(character, data.Value);
            }
            return true;
        }
    }
}
