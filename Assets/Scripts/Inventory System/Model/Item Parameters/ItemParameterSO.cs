using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu(menuName ="Inventory System/Item Parameters", fileName ="new ItemParameter")]
    public class ItemParameterSO : ScriptableObject
    {
        [field: SerializeField]
        public string ParameterName { get; private set; }
    }
}