using UnityEngine;

namespace _Project.Script.AbilitySystem._New
{
    [CreateAssetMenu(menuName = "Data/New Stat")]
    public class UnitStatSO : ScriptableObject
    {
        public float speed;
        public float maxHealth;
        public float baseDamage;
        public float baseDefense;

    }
}