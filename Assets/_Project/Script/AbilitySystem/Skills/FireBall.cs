using UnityEngine;

namespace _Project.Script.AbilitySystem
{
    public class FireBall : Ability
    {
        [Header("Skill Settings")] 
        [SerializeField] private int damage;
        [SerializeField] private int duration;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject fireBallPrefab;
    }
}