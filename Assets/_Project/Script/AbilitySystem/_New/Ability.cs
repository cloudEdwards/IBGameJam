using System;
using _Project.Script.AbilitySystem.Dudduruu;
using UnityEngine;

namespace _Project.Script.AbilitySystem._New
{
    public class Ability
    {
        private AbilityBaseSO _abilityBaseSo;
        
        private float _lastUsedTime = -999f;
        private float _currentCoolDown = 0;
        
        private float _holdTimer = 0;
        private bool _isHelding;
        private bool _canHeld;
        private int _pressCount = 0;
        private bool _multiPressActive;
        private float _timer;

        private Modifier _freezeModifier;
        private bool _hasModifier;
        
        public float RemainingTime => Mathf.Abs(Time.time - (_lastUsedTime + _currentCoolDown));
        public float RemainingTimeInverse => Mathf.InverseLerp(_currentCoolDown, 0, RemainingTime);

        public Ability(AbilityBaseSO abilityBaseSO)
        {
            _abilityBaseSo = abilityBaseSO;
            _freezeModifier = new Modifier(0, ModifierType.Multiplicative).SetStatType(StatType.Speed);
        }
        
        public virtual bool CanUse()
        {
            if (_abilityBaseSo == null) return false;
            if (Time.time < _lastUsedTime + _currentCoolDown) return false; // Cool_Down Check

            return true;
        }

        public virtual void TryUse(BaseUnit caster, bool keyValue)
        {
            if (!CanUse()) return;
            
            switch (_abilityBaseSo.abilityInputType)
            {
                case AbilityInputType.Press:
                    if (keyValue)
                    {
                        _abilityBaseSo.UseAbility(caster);
                        StartCooldown(_abilityBaseSo.cooldown);
                    }
                    break;
                case AbilityInputType.HoldToCharge:
                    if (!keyValue)
                    {
                        if (_isHelding)
                        {
                            _abilityBaseSo.UseAbility(caster);
                            StartCooldown(_abilityBaseSo.cooldown);
                            _isHelding = false;
                            _holdTimer = 0;
                            caster.RemoveModifier(_freezeModifier);
                            Debug.Log(caster.UnitStats.Speed.Value);

                            _hasModifier = false;
                            Debug.Log("Modifier Removed");
                        }
                        else
                            _canHeld = true;
                    }
                    else if (_canHeld)
                    {
                        _isHelding = true;
                        _holdTimer += Time.deltaTime;
                        if (!_abilityBaseSo.canMoveWhileCharging && !_hasModifier)
                        {
                            // TODO Somehow it does't apply this modifier here???
                            caster.AddModifier(_freezeModifier.Apply());
                            Debug.Log("Modifier Added");
                            Debug.Log(caster.UnitStats.Speed.Value);
                            _hasModifier = true;
                        }
                        if (_abilityBaseSo.hasMaxDuration && _holdTimer >= _abilityBaseSo.maxHoldDuration)
                        {
                            _abilityBaseSo.UseAbility(caster);
                            StartCooldown(_abilityBaseSo.cooldown);
                            _holdTimer = 0;
                            _isHelding = false;
                            _canHeld = false;
                            caster.RemoveModifier(_freezeModifier);
                            _hasModifier = false;
                        }
                    }

                    break;
                case AbilityInputType.MultiPress:
                    if (keyValue)
                    {
                        _pressCount++;

                        if (_pressCount <= _abilityBaseSo.maxPressCount)
                        {
                            _multiPressActive = true;
                            _abilityBaseSo.UseAbility(caster);
                            StartCooldown(_abilityBaseSo.durationBetweenClicks);
                        }
                        else
                        {
                            _multiPressActive = false;
                            StartCooldown(_abilityBaseSo.cooldown);
                            _pressCount = 0;
                        }
                    }
                    else
                    {
                        if (_multiPressActive)
                        {
                            _timer += Time.deltaTime;

                            if (_timer >= _abilityBaseSo.durationBetweenClicks)
                            {
                                _multiPressActive = false;
                                _timer = 0;
                                StartCooldown(_abilityBaseSo.cooldown);
                                _pressCount = 0;
                            }
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void StartCooldown(float duration)
        {
            _currentCoolDown = duration;
            _lastUsedTime = Time.time;
        }
    }
}