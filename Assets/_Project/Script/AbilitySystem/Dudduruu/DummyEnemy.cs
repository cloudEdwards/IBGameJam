using _Project.Script.Persistent;
using UnityEngine;
using VInspector;

namespace _Project.Script.AbilitySystem.Dudduruu
{
    public class DummyEnemy : BaseUnit
    {
        private float timer;
        private int multip = 1;
        
        public override void OnAwake()
        {
            base.OnAwake();
            targetType = AbilityTargeting.Hostile;
        }
        
        public override Vector3 GetMousePoint()
        {
            return transform.position + transform.forward;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= 5)
            {
                multip *= -1;
                timer = 0;
            }

            currentHealth = CurrentHealth;
            transform.position += UnitStats.Speed * multip * transform.right * Time.deltaTime;
        }

        public override Vector3 GetDirection()
        {
            return transform.forward;
        }

        public override BaseUnit GetSelectedTarget()
        {
            return FindFirstObjectByType<PlayerUnit>();
        }

        [Button]
        public void GetHit()
        {
            PersistentManagerHolderSO.Instance.damageTextManager.CreateDamageText(transform.position + Vector3.up * 2, 100);
        }
    }
}