using DG.Tweening;
using UnityEngine;

namespace _Project.Script.Persistent
{
    public class SoundFXPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        public void Initialize(AudioClip clip, float volume, float maxDistance)
        {
            _audioSource.PlayOneShot(clip, volume);
            _audioSource.maxDistance = maxDistance;

            Sequence sequence = DOTween.Sequence();
            sequence.SetLink(gameObject);
            sequence.AppendInterval(clip.length);
            sequence.AppendCallback(() => Destroy(gameObject));
        }
    }
}