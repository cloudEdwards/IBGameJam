using _Project.Script.Persistent;
using UnityEngine;
using VInspector;

namespace _Project.Script.AbilitySystem.Dudduruu
{
    public class DummyEnemy : BaseUnit
    {
        public override void OnAwake()
        {
            base.OnAwake();
            TargetType = AbilityTargeting.Hostile;
        }

        public override void TakeDamage(int damageAmount)
        {
            base.TakeDamage(damageAmount);
            Debug.Log(damageAmount);
        }
        
        [Button]
        public void GetHit()
        {
            PersistentManagerHolderSO.Instance.damageTextManager.CreateDamageText(transform.position + Vector3.up * 2, 100);
        }
    }
}