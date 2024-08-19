using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName = "Inventory System/Item", fileName ="New ItemSO")]
    public class ItemSO : ScriptableObject
    {
        public int ID => GetInstanceID();

        [field: SerializeField] // if > 1 item becomes stacklable
        public int MaxStackSize { get; set; } = 1;

        // if you populate descriptions and names from JSON files:
        // you can remove TextArea from Description, add the Name property back
    
        //[field: SerializeField]
        //public string Name { get; set; }

        [field: SerializeField, TextArea] 
        public string Description { get; set; }

        [field: SerializeField]
        public Sprite ItemSprite {  get; set; }
    }
}