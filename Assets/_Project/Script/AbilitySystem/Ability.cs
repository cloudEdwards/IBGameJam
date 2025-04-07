using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Script.AbilitySystem
{
    public enum ModifierType
    {
        Additive,
        Multiplicative
    }
    [Serializable]
    public class Modifier
    {
        public float BaseValue;
        public ModifierType ModifierType;

        public bool HasDuration = false;
        public float Duration = -1;
        public float CreatedTime;

        public Modifier(float baseValue, ModifierType modifierType)
        {
            BaseValue = baseValue;
            ModifierType = modifierType;
            HasDuration = false;
            Duration = -1;
            CreatedTime = Time.time;
        }
        
        public Modifier(float baseValue, ModifierType modifierType, float duration)
        {
            BaseValue = baseValue;
            ModifierType = modifierType;
            HasDuration = true;
            Duration = duration;
            CreatedTime = Time.time;
        }
    }
    
    public struct kfloat
    {
        public float BaseValue;
        public float Value => CalculateFinal();
        
        private List<Modifier> _modifiers;
        
        public kfloat(float baseValue)
        {
            BaseValue = baseValue;
            _modifiers = new List<Modifier>();
        }

        public float CalculateFinal()
        {
            float additive = 0f;
            float multiplicative = 1f;

            for (int i = 0; i < _modifiers.Count - 1; i++)
            {
                var mod = _modifiers[i];

                if (mod.HasDuration && Time.time >= mod.CreatedTime + mod.Duration)
                {
                    _modifiers.RemoveAt(i);
                    continue;
                }

                if (mod.ModifierType == ModifierType.Additive)
                    additive += mod.BaseValue;
                else
                    multiplicative *= mod.BaseValue;

            }

            float final = (BaseValue + additive) * multiplicative;
            return final > 0 ? final : 0;
        }
        
        public void AddModifier(Modifier newModifier)
        {
            _modifiers.Add(newModifier);
        }

        public void RemoveModifier(Modifier modifier)
        {
            if (_modifiers.Contains(modifier)) _modifiers.Remove(modifier);
        }

        public void CleanModifiers()
        {
            _modifiers.Clear();
        }

        public static implicit operator kfloat(float value) => new kfloat(value);
        public static implicit operator float(kfloat kf) => kf.Value;
    }
    public class EntityStats
    {
        public kfloat Speed;
        public kfloat MaxHealth;
        public kfloat BaseDamage;
        public kfloat BaseDefense;
    }
    
    public interface IAbility
    {
        public void OnButtonDown();
        public void OnButtonHeld();
        public void OnButtonUp();
    }

    [Flags]
    public enum AbilityTargeting
    {
        None     = 0,
        Self     = 1 << 0,
        Friendly = 1 << 1,
        Hostile  = 1 << 2,
        All      = ~(~0 << 4)
    }
    
    public abstract class Ability : MonoBehaviour, IAbility
    {
        [Header("Ability Settings")] 
        public string Name;
        public Sprite Icon;
        public float Cooldown;
        public AbilityTargeting Target;
        
        private float lastUsedTime = -999f;
        protected bool isFinished = false;

        public bool IsActive => enabled;
        public bool IsFinished => isFinished;
        public float RemainingTime => Mathf.Abs(Time.time - (lastUsedTime + Cooldown));
        public float RemainingTimeInverse => Mathf.InverseLerp(Cooldown, 0, RemainingTime);
        public virtual bool CanUse()
        {
            return Time.time >= lastUsedTime + Cooldown;
        }

        public void SetActivate(bool isActive)
        {
            enabled = isActive;
        }

        public void StartCooldown()
        {
            lastUsedTime = Time.time;
        }

        private void OnEnable()
        {
            Prepare();
        }

        private void OnDisable()
        {
            Dispose();
        }

        private void Update()
        {
            OnUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            OnFixedUpdate(Time.deltaTime);
        }

        public virtual void Prepare(){}
        public virtual void Dispose(){}
        public virtual void OnUpdate(float deltaTime){}
        public virtual void OnFixedUpdate(float deltaTime){}
        
        public virtual void OnButtonDown(){}
        public virtual void OnButtonHeld(){}
        public virtual void OnButtonUp(){}
        public virtual void Cancel(){}
    }

    public enum DamageApplicationType
    {
        Instant,
        OverTime,
        Periodic,
    }

    public enum DamageType
    {
        Fire,
        Poison,
        Magic,
        Bleed,
        Shock,
        Cold,
        Burst,
    }

    public interface IDamagable
    {
        public AbilityTargeting TargetType { get; }
        public void TakeDamage(int damageAmount);
    }
}