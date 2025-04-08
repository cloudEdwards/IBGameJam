using DG.Tweening;
using UnityEngine;

namespace _Project.Script.AbilitySystem.Dudduruu
{
    public abstract class BaseUnit : MonoBehaviour, IStat, IDamagable
    {
        public EnemyStat Stat { get; set; }
        private float CurrentHealth;
        
        public AbilityTargeting TargetType { get; protected set; }


        private void Awake()
        {
            OnAwake();
            Stat = new EnemyStat(1, 100, 1, 1);
        }

        public virtual void OnAwake()
        {
            CurrentHealth = Stat.MaxHealth;
        }
        
        public virtual void TakeDamage(int damageAmount)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damageAmount, Stat.MaxHealth, 0);
        }

        public virtual void OnDeath()
        {
            transform.DOMove(Vector3.down * 5, 5f).SetLink(gameObject).OnComplete(() => Destroy(gameObject));
        }
    }
}