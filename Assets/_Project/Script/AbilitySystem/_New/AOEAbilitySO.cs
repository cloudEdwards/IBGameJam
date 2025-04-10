using _Project.Script.AbilitySystem.Dudduruu;
using _Project.Script.Persistent;
using Unity.Mathematics;
using UnityEngine;
using VInspector;

namespace _Project.Script.AbilitySystem._New
{
    [CreateAssetMenu(menuName = "Ability/New AOE")]
    public class AOEAbilitySO : AbilityBaseSO
    {
        [Header("AOE Setting")] 
        public AbilityTargeting abilityTarget;
        public DamageApplicationType damageApplicationType;
        public float damageAmount;
        public float radius;
        [HideIf("damageApplicationType", DamageApplicationType.Instant)]
        [Range(0.5f, 10f)]
        public float duration;
        [Range(0.1f, 3)]
        public float tickRate;
        [HideIf("", true)]
        public float maxDistance;
        public GameObject prefab;
        
        
        public override void UseAbility(BaseUnit caster)
        {
            Vector3 direction = caster.GetMousePoint() - caster.transform.position;
            direction.y = 0;
            direction.Normalize();

            float distance = Vector3.Distance(caster.GetMousePoint().SetValues(y:0f), caster.transform.position.SetValues(y:0f));
            GameObject prefabObj = Instantiate(prefab, distance > maxDistance ? caster.transform.position.SetValues(y:0) + (direction * maxDistance) : caster.GetMousePoint(), quaternion.identity);

            if (damageApplicationType == DamageApplicationType.Instant)
            {
                prefabObj.AddComponent<Ability_DamageGiverOnHit>()
                    .Initialize(abilityTarget, radius, damageAmount)
                    .SetCaster(caster)
                    .AddHitModifiers(modifiersOnTargetHit);
            }
            else if (damageApplicationType == DamageApplicationType.Periodic)
            {
                prefabObj.AddComponent<Ability_DamageGiverOverTime>()
                    .Initialize(duration, tickRate, damageAmount, radius)
                    .Initialize(abilityTarget, radius, damageAmount)
                    .SetCaster(caster)
                    .AddHitModifiers(modifiersOnTargetHit);
            }
        }
    }
}