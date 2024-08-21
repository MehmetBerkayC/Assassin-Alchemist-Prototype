using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour // Make a Stat system
{
    [field: SerializeField]
    public float CurrentHealth { get; private set; } = 60;

    public void AddHealth(float value)
    {
        CurrentHealth += value;
        Debug.Log(CurrentHealth);
    }
}
