using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Script.Persistent
{
    [CreateAssetMenu(fileName = "Damage Text Manager", menuName = "Manager/Damage Text Manager")]
    public class DamageTextManagerSO : ScriptableObject
    {
        [FormerlySerializedAs("DamagePopUpPrefab")] [SerializeField] private GameObject _damagePopUpPrefab;

        public void CreateDamageText(Vector3 position, float damageAmount)
        {
            var damageObj = Instantiate(_damagePopUpPrefab, position, quaternion.identity);
            DamageTextPopup damageText = damageObj.GetComponent<DamageTextPopup>();
            damageText.Initialize(damageAmount.ToString("N0"), Color.white, 1);
        }
    }
}