using UnityEngine;

namespace _Project.Script.AbilitySystem
{
    public class OrbitalHavoc : Ability
    {
        [Header("Skill Settings")] 
        [SerializeField] private int damage;
        [SerializeField] private int duration;
        [SerializeField] private GameObject orbitalPrefab;

        public override void OnButtonDown()
        {
        }
    }
}