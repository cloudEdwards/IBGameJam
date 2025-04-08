using Unity.Mathematics;
using UnityEngine;

namespace _Project.Script.Persistent
{
    [CreateAssetMenu(fileName = "SoundFX Manager", menuName = "Manager/SoundFX Manager")]
    public class SoundFXManager : ScriptableObject
    {
        [SerializeField] private GameObject _soundFXPrefab;

        public void PlaySFX(Vector3 position, AudioClip clip)
        {
            var sfx = Instantiate(_soundFXPrefab, position, quaternion.identity);
            SoundFXPlayer player = sfx.GetComponent<SoundFXPlayer>();
            player.Initialize(clip, 1, 10);
        }
    }
}