using _Project.Script.AbilitySystem._New;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Script.AbilitySystem
{
    public class AbilityHolderUI : MonoBehaviour
    {
        [SerializeField] private Image _skilLFillImage;
        [SerializeField] private Image _abilityIcon;
        [SerializeField] private TextMeshProUGUI _cooldownText;

        [SerializeField] private AbilityBaseSO _abilityBaseSo;
        
        private Ability _ability;

        public Ability GetAbility => _ability;
        
        public void Bind(AbilityBaseSO ability)
        {
            _ability = new Ability(ability);
            _abilityIcon.sprite = ability.abilityIcon;
            _abilityIcon.enabled = true;
        }

        private void Start()
        {
            _cooldownText.text = "";
            _skilLFillImage.enabled = false;
            _abilityIcon.enabled = false;
            
            if(_abilityBaseSo != null)
                Bind(_abilityBaseSo);
        }

        private void Update()
        {
            if (_ability == null)
                return;

            if (_ability.CanUse())
            {
                _cooldownText.text = "";
                _skilLFillImage.enabled = false;
            }
            else
            {
                _skilLFillImage.enabled = true;
                _skilLFillImage.fillAmount = _ability.RemainingTimeInverse;
                _cooldownText.text = _ability.RemainingTime.ToString("F1");
            }
        }

        public void Reset()
        {
            _ability = null;
            _abilityIcon.sprite = null;
            _abilityIcon.enabled = false;
        }
    }
}