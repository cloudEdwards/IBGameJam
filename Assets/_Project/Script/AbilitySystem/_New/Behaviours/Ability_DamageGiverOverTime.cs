using System.Collections;
using _Project.Script.AbilitySystem.Dudduruu;
using UnityEngine;

namespace _Project.Script.AbilitySystem._New
{
    public class Ability_DamageGiverOverTime : Ability_DamageGiverBase
    {
        private float _duration;
        private float _tickRate;
        
        public Ability_DamageGiverOverTime Initialize(float duration, float tickRate, float damageAmount, float radius)
        {
            _duration = duration;
            _tickRate = tickRate;
            _damageAmount = damageAmount;
            _radius = radius;
            
            StartCoroutine(ApplyPeriodicDamage());
            return this;
        }
        
        private IEnumerator ApplyPeriodicDamage()
        {
            float elapsed = 0f;
            float damagePerTick = _damageAmount / _duration * _tickRate;

            while (elapsed <= _duration)
            {
                var hits = Physics.OverlapSphere(transform.position, _radius);
                foreach (var hit in hits)
                {
                    if (hit.transform.TryGetComponent(out BaseUnit unit))
                    {
                        Debug.Log(damagePerTick);
                        DealDamage(unit, damagePerTick);
                    }
                }

                yield return new WaitForSeconds(_tickRate);
                elapsed += _tickRate;
            }

            Destroy(gameObject);
        }
        
        private void ApplyInstantDamage()
        {
            var hits = Physics.OverlapSphere(transform.position, _radius);
            foreach (var hit in hits)
            {
                if (hit.transform.TryGetComponent(out BaseUnit unit))
                {
                    DealDamage(unit, _damageAmount);
                }
            }
        }
    }
}