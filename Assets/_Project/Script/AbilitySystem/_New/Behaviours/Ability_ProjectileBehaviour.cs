using System;
using _Project.Script.AbilitySystem.Dudduruu;
using UnityEngine;

namespace _Project.Script.AbilitySystem._New
{
    public class Ability_ProjectileBehaviour : MonoBehaviour
    {
        private ProjectileAbilitySO.ProjectileType _projectileType;
        private float _speed;
        private float _lifeTime;

        private float _timer;

        private BaseUnit target;
        private Vector3 shootDirection;
        private Vector3 mousePoint;
        private Vector3 startPosition;
        
        public void Initialize(BaseUnit caster, ProjectileAbilitySO abilitySo)
        {
            _projectileType = abilitySo.projectileType;
            _speed = abilitySo.speed;
            _lifeTime = abilitySo.lifeTime;

            startPosition = caster.transform.position;

            switch (abilitySo.projectileType)
            {
                case ProjectileAbilitySO.ProjectileType.Directional:
                    shootDirection = caster.GetDirection();
                    shootDirection.y = 0;
                    shootDirection.Normalize();
                    break;
                case ProjectileAbilitySO.ProjectileType.OnMousePoint:
                    mousePoint = caster.GetMousePoint();
                    break;
                case ProjectileAbilitySO.ProjectileType.Targeted:
                    target = caster.GetSelectedTarget();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Update()
        {
            switch (_projectileType)
            {
                case ProjectileAbilitySO.ProjectileType.Directional:
                    transform.position += shootDirection * _speed * Time.deltaTime;
                    break;
                case ProjectileAbilitySO.ProjectileType.OnMousePoint:
                    Vector3 direction = mousePoint - startPosition;
                    direction.y = 0;
                    transform.position += direction.normalized * _speed * Time.deltaTime;
                    break;
                case ProjectileAbilitySO.ProjectileType.Targeted:
                    Vector3 dir = target.transform.position - transform.position;
                    dir.y = 0;
                    transform.position += dir.normalized * _speed * Time.deltaTime;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            _timer += Time.deltaTime;

            if (_timer >= _lifeTime)
                Destroy(gameObject);
        }
    }
}