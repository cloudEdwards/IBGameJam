using _Project.Script.AbilitySystem.Dudduruu;
using UnityEngine;

namespace _Project.Script.AbilitySystem._New
{
    public class Ability_DamageGiverOnHit : Ability_DamageGiverBase
    {
        protected void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BaseUnit unit) && IsValidTarget(unit))
            {
                DealDamage(unit, _damageAmount);
                
                foreach (var mod in _modifiers)
                    unit.AddModifier(mod);
                
                Destroy(gameObject);
            }
        }
    }
}