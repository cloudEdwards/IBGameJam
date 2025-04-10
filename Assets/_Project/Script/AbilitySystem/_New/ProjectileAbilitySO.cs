using _Project.Script.AbilitySystem.Dudduruu;
using Unity.Mathematics;
using UnityEngine;

namespace _Project.Script.AbilitySystem._New
{
    [CreateAssetMenu(menuName = "Ability/New Projectile")]
    public class ProjectileAbilitySO : AbilityBaseSO
    {
        public enum ProjectileType
        {
            Directional,
            OnMousePoint,
            Targeted,
        }

        [Header("Projectile Settings")]
        public ProjectileType projectileType;
        public AbilityTargeting abilityTarget;
        public float lifeTime;
        public float hitRadius;
        public float speed;
        public float damage;
        public GameObject prefab;
        
        // TODO Add Sound FX
        
        public override void UseAbility(BaseUnit caster)
        {
            GameObject prefabObj = Instantiate(prefab, caster.transform.position, quaternion.identity);
            
            prefabObj.AddComponent<Ability_DamageGiverOnHit>()
                .Initialize(abilityTarget, hitRadius, damage)
                .AddHitModifiers(modifiersOnTargetHit)
                .SetCaster(caster);

            foreach (var mod in modifiersOnCaster)
            {
                caster.AddModifier(mod.Apply());
            }
            
            prefabObj.AddComponent<Ability_ProjectileBehaviour>().Initialize(caster, this);

            foreach (var mod in modifiersOnCaster)
                caster.AddModifier(mod);
        }
    }
}