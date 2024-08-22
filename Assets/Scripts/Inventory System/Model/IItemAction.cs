using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    public interface IItemAction
    {
        public string ActionName { get; }
        public AudioClip ActionSFX { get; }
        bool PerformAction(GameObject character, List<ItemParameter> itemState);
    }
}
