using System.Collections.Generic;
using _Project.Script.AbilitySystem.Dudduruu;
using UnityEngine;

namespace _Project.Script.AbilitySystem._New
{
    [RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
    public abstract class Ability_DamageGiverBase : MonoBehaviour
    {
        protected BaseUnit _caster;
        protected List<Modifier> _modifiers;
        protected AbilityTargeting _targetType;
        protected float _radius;
        protected float _damageAmount;

        public Ability_DamageGiverBase Initialize(AbilityTargeting targetType, float radius, float damageAmount)
        {
            _targetType = targetType;
            _radius = radius;
            _damageAmount = damageAmount;
            
            var sphere = GetComponent<SphereCollider>();
            sphere.radius = radius;
            sphere.isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;
            
            return this;
        }
        
        protected void DealDamage(BaseUnit unit, float damageAmount)
        {
            if (IsValidTarget(unit))
            {
                unit.TakeDamage(damageAmount + _caster.UnitStats.BaseDamage);
                foreach (var mod in _modifiers)
                {
                    unit.AddModifier(mod.Apply());
                }
            }
        }
        
        protected bool IsValidTarget(BaseUnit unit)
        {
            if (unit == null) return false;
            return _targetType.HasFlag(unit.TargetType); // TODO there is bug. It doesn't check all the flags
        }

        public Ability_DamageGiverBase SetCaster(BaseUnit caster)
        {
            _caster = caster;
            return this;
        }
        
        public Ability_DamageGiverBase AddHitModifiers(List<Modifier> modifiers)
        {
            _modifiers = modifiers;
            return this;
        }
    }
}