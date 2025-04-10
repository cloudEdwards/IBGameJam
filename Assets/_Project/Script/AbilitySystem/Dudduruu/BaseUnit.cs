using System;
using _Project.Script.AbilitySystem._New;
using _Project.Script.Persistent;
using DG.Tweening;
using UnityEngine;

namespace _Project.Script.AbilitySystem.Dudduruu
{
    public abstract class BaseUnit : MonoBehaviour, IUnitStat, IDamagable
    {
        [SerializeField] private UnitStatSO _unitStatsSO;
        
        private UnitStats _unitStats;
        public UnitStats UnitStats => _unitStats;
        protected float CurrentHealth;

        public float currentHealth;

        [SerializeField] protected AbilityTargeting targetType;
        public AbilityTargeting TargetType => targetType;

        private void Awake()
        {
            _unitStats = new UnitStats(_unitStatsSO);
            CurrentHealth = UnitStats.MaxHealth;
            OnAwake();
        }

        public virtual void OnAwake()
        {
        }
        
        public virtual void TakeDamage(float damageAmount)
        {
            float finalDamage = damageAmount - _unitStats.BaseDefense;
            finalDamage = finalDamage < 0 ? 0 : finalDamage;
            
            Debug.Log("Took Damage : " + finalDamage);
            CurrentHealth = Mathf.Clamp(CurrentHealth - finalDamage, 0, _unitStatsSO.maxHealth);
            PersistentManagerHolderSO.Instance.damageTextManager.CreateDamageText(transform.position, finalDamage);
        }

        public virtual void OnDeath()
        {
            transform.DOMove(Vector3.down * 5, 5f).SetLink(gameObject).OnComplete(() => Destroy(gameObject));
        }

        public virtual void AddModifier(Modifier modifier)
        {
            switch (modifier.StatType)
            {
                case StatType.Speed:
                    UnitStats.Speed.AddModifier(modifier);
                    break;
                case StatType.MaxHealth:
                    UnitStats.MaxHealth.AddModifier(modifier);
                    break;
                case StatType.Damage:
                    UnitStats.BaseDamage.AddModifier(modifier);
                    break;
                case StatType.Defense: 
                    UnitStats.BaseDefense.AddModifier(modifier);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public virtual void RemoveModifier(Modifier modifier)
        {
            switch (modifier.StatType)
            {
                case StatType.Speed:
                    UnitStats.Speed.RemoveModifier(modifier);
                    break;
                case StatType.MaxHealth:
                    UnitStats.MaxHealth.RemoveModifier(modifier);
                    break;
                case StatType.Damage:
                    UnitStats.BaseDamage.RemoveModifier(modifier);
                    break;
                case StatType.Defense: 
                    UnitStats.BaseDefense.RemoveModifier(modifier);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public virtual void ClearModifiers()
        {
            UnitStats.Speed.CleanModifiers();
            UnitStats.MaxHealth.CleanModifiers();
            UnitStats.BaseDamage.CleanModifiers();
            UnitStats.BaseDefense.CleanModifiers();
        }


        public abstract Vector3 GetMousePoint();
        public abstract Vector3 GetDirection();
        public abstract BaseUnit GetSelectedTarget();
    }
}