using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Script.AbilitySystem
{
    public class AbilityHolderUI : MonoBehaviour
    {
        [SerializeField] private Image _skilLFillImage;
        [SerializeField] private Image _abilityIcon;
        [SerializeField] private TextMeshProUGUI _cooldownText;
        
        [Header("Ability Settings")]
        [SerializeField] private Ability _ability;

        public Ability GetAbility => _ability;
        
        public void Bind(Ability ability)
        {
            _ability = ability;
            _abilityIcon.sprite = ability.Icon;
        }

        private void Start()
        {
            _cooldownText.text = "";
            _skilLFillImage.enabled = false;
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
        }
    }
}