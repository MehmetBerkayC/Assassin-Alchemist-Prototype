using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CraftingItemContainer
{
    public CraftingItemData ItemData;
    
    [Range(1,99)] 
    public int Amount = 1;
}
