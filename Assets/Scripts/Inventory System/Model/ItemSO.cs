using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    public abstract class ItemSO : ScriptableObject
    {
        public int ID => GetInstanceID();

        [field: SerializeField] // Shouldn't need it but just in case
        public bool IsStackable { get; private set; }

        [field: SerializeField] // if > 1 item becomes stacklable
        public int MaxStackSize { get; private set; } = 1;
        
        // if you populate descriptions and names from JSON files:
        // you can remove TextArea from Description, add the Name property back
    
        //[field: SerializeField]
        //public string Name { get; private set; }

        [field: SerializeField, TextArea] 
        public string Description { get; private set; }

        [field: SerializeField]
        public Sprite ItemSprite {  get; private set; }
    }
}