using System.Collections.Generic;
using _Project.Script.AbilitySystem.Dudduruu;
using UnityEngine;
using VInspector;

namespace _Project.Script.AbilitySystem._New
{
    public abstract class AbilityBaseSO : ScriptableObject
    {
        [Header("General")] 
        public string abilityName;
        [TextArea(3,10)]
        public string description;
        public Sprite abilityIcon;
        public float cooldown;

        // ********************************* //
        [Header("Input Settings")] 
        public AbilityInputType abilityInputType;
        // Press
            // Nothing
            
        // Hold To Charge
        [ShowIf("abilityInputType", AbilityInputType.HoldToCharge)]
        public bool canMoveWhileCharging;
        public bool hasMaxDuration;
        private bool _showHoldDuration => hasMaxDuration && abilityInputType == AbilityInputType.HoldToCharge;
        [ShowIf("_showHoldDuration", true)]
        [Range(0f, 10f)]
        public float maxHoldDuration;
        
        // Multi_Pressdwa
        [Min(2)]
        [ShowIf("abilityInputType", AbilityInputType.MultiPress)]
        public int maxPressCount = 2;
        public float durationBetweenClicks;

        [HideIf("", true)]
        [Header("Modifier Settings")] 
        public List<Modifier> modifiersOnTargetHit;
        public List<Modifier> modifiersOnCaster;
        

        public abstract void UseAbility(BaseUnit caster);
    }
}