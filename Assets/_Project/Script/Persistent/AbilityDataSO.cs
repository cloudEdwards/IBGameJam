// using System;
// using _Project.Script.AbilitySystem;
// using UnityEngine;
// using VInspector;
//
// namespace _Project.Script.Persistent
// {
//     [CreateAssetMenu(fileName = "New Ability Data", menuName = "Ability/Create Ability Data")]
//     public class AbilityDataSO : ScriptableObject
//     {
//         [Header("General")] public string abilityName;
//         public Sprite abilityIcon;
//
//         // ********************************* //
//         [Header("Input Settings")] public AbilityInputType abilityInputType;
//         // Press
//             // Nothing
//             
//         // Hold To Charge
//         [ShowIf("abilityInputType", AbilityInputType.HoldToCharge)]
//         public bool canMoveWhileCharging;
//         public bool hasMaxDuration;
//         private bool _showHoldDuration => hasMaxDuration && abilityInputType == AbilityInputType.HoldToCharge;
//         [Range(0f, 10f)]
//         [ShowIf("_showHoldDuration", true)]
//         public float maxHoldDuration;
//         
//         // Multi_Press
//         [ShowIf("abilityInputType", AbilityInputType.MultiPress)]
//         [Min(2)]
//         public int maxPressCount = 2;
//         public float durationBetweenClicks;
//
//         // ********************************* //
//         [HideIf("", true)] 
//         [Header("Ability Settings")] 
//         public float cooldown;
//         [Tooltip("Max Damage Amount")]
//         public float damageAmount;
//         [Range(0.1f, 10f)]
//         [Tooltip("Impact point Radius")]
//         public float hitRadius = 0.1f;
//         [Range(0.1f, 20)]
//         public float lifeTime = 5;
//         public DamageType damageType;
//         public AbilityTargeting abilityTarget;
//         public DamageApplicationType applicationType;
//
//         [ShowIf("applicationType",  DamageApplicationType.OverTime)]
//         [Tooltip("How often damage is applied (in seconds). If Damage : 10, Interval : 0.25 : then Every Tick deals 2.5 damage.")]
//         public float damageInterval;
//
//         [HideIf("", true)] 
//         public AbilityType abilityType;
//
//         // Upgrade Options
//         
//         
//         // ********************************* //
//         [HideIf("", true)] 
//         [Header("Animation Settings")] 
//         public AnimationClip OnCast;
//         public AnimationClip OnRelease;
//
//         public GameObject prefab;
//
//     }
//
//     [Serializable]
//     public class ParticleEffect
//     {
//         public GameObject particleEffectPrefab;
//         public float startScale = 1;
//         public bool scaleOverTime;
//         public float endScale = 1;
//     }
// }