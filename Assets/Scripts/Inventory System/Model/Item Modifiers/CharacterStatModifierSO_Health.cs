using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName ="Inventory System/Stat Modifiers/Health", fileName ="new HealthModifier")]
public class CharacterStatModifierSO_Health : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float value)
    {
        Health health = character.GetComponent<Health>();
        if (health != null)
        {
            health.AddHealth(value);
        }
    }
}
