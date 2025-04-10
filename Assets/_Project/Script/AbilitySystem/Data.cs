using System;
using System.Collections.Generic;
using _Project.Script.AbilitySystem._New;
using UnityEngine;
using UnityEngine.Serialization;
using VInspector;

    public enum ModifierType
    {
        Additive,
        Multiplicative
    }


    public enum StatType
    {
        Speed,
        MaxHealth,
        Damage,
        Defense,
    }


    [Serializable]
    public class Modifier
    {
        public float BaseValue;
        public ModifierType ModifierType;
        public StatType StatType;

        public bool HasDuration = false;
        public float Duration = -1;
        private float _appliedTime = -999;
        public float AppliedTime
        {
            get
            {
                return _appliedTime;
            }
            set
            {
                _appliedTime = value;
            }
        }

        public Modifier(float baseValue, ModifierType modifierType)
        {
            BaseValue = baseValue;
            ModifierType = modifierType;
            HasDuration = false;
            Duration = -1;
        }
        
        public Modifier(float baseValue, ModifierType modifierType, float duration)
        {
            BaseValue = baseValue;
            ModifierType = modifierType;
            HasDuration = true;
            Duration = duration;
        }

        public Modifier SetStatType(StatType statType)
        {
            StatType = statType;
            return this;
        }

        public Modifier Apply()
        {
            _appliedTime = Time.time;
            return this;
        }
    }
    
    [Serializable]
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

            if (_modifiers.Count == 0) return BaseValue;
            
            for (int i = 0; i < _modifiers.Count - 1; i++)
            {
                var mod = _modifiers[i];

                if (mod.HasDuration && Time.time >= mod.AppliedTime + mod.Duration)
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

        public int GetModifierCount() => _modifiers.Count;

        public static implicit operator kfloat(float value) => new kfloat(value);
        public static implicit operator float(kfloat kf) => kf.Value;
    }

    public interface IUnitStat
    {
        public UnitStats UnitStats { get; }
    }
    
    [Serializable]
    public class UnitStats
    {
        public kfloat Speed;
        public kfloat MaxHealth;
        public kfloat BaseDamage;
        public kfloat BaseDefense;

        public UnitStats(float speed, float maxHealth, float baseDamage, float baseDefense)
        {
            Speed = new kfloat(speed);
            MaxHealth = new kfloat(maxHealth);
            BaseDamage = new kfloat(baseDamage);
            BaseDamage = new kfloat(baseDefense);
        }

        public UnitStats(UnitStatSO unitStatSo)
        {
            Speed = unitStatSo.speed;
            MaxHealth = unitStatSo.maxHealth;
            BaseDamage = unitStatSo.baseDamage;
            BaseDefense = unitStatSo.baseDefense;
        }
    }
    
    [Flags]
    public enum AbilityTargeting
    {
        None     = 0,
        Self     = 1 << 0,
        Friendly = 1 << 1,
        Hostile  = 1 << 2,
    }

    public enum AbilityType
    {
        Directional,      // Fires in forward Direction
        OnMousePoint,     // Cast on Mouse Point
        Targeted,         // Requires mouse to be on target
    }


    public enum AbilityInputType
    {
        Press,          // Instant Cast
        HoldToCharge,   // Hold To Cast
        MultiPress,     // Cast Multiple Times
    }

    public enum DamageApplicationType
    {
        Instant,
        Periodic,
    }

    // TODO Implement Damage Types
    public enum DamageType
    {
        None,
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
        public void TakeDamage(float damageAmount);
    }
