using System;
using UnityEngine;

namespace _Project.Script.AbilitySystem
{
    public interface IAbility
    {
        public void OnButtonDown();
        public void OnButtonHeld();
        public void OnButtonUp();
    }

    [Flags]
    public enum AbilityTargeting
    {
        None      = 0,
        Self      = 1 << 0,
        Friendly  = 1 << 1,
        Hostile   = 1 << 2,
        All       = ~(~0 << 4)
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
        private bool isActive = false;

        public bool IsActive => isActive;
        public bool IsFinished => isFinished;
        public float RemainingTime => Mathf.Abs(Time.time - (lastUsedTime + Cooldown));
        public float RemainingTimeInverse => Mathf.InverseLerp(Cooldown, 0, RemainingTime);
        public virtual bool CanUse()
        {
            return Time.time >= lastUsedTime + Cooldown;
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