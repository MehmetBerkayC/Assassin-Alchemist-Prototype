using System;

namespace Inventory.Model
{
    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter> // This interface is required to have a parameter list
    {
        public ItemParameterSO Parameter;
        public float Value;

        public bool Equals(ItemParameter other)
        {
            return other.Parameter == Parameter;
        }
    }
}