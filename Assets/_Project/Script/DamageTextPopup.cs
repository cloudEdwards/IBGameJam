using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Script.Persistent
{
    public class DamageTextPopup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private AnimationCurve _alphaCurve;
        [SerializeField] private AnimationCurve _scaleCurve;

        private float timer;

        public void Initialize(string damageString, Color color, float duration)
        {
            _textMesh.text = damageString;
            _textMesh.color = color;
            timer = 0;

            _canvasGroup.alpha = _alphaCurve.Evaluate(0);
            RectTransform rectTransform = transform as RectTransform;
            rectTransform.localScale = Vector3.one * _scaleCurve.Evaluate(0);

            rectTransform.DOMove(rectTransform.position + Vector3.up * 1, duration)
                .SetLink(gameObject)
                .OnUpdate(() =>
                {
                    timer += Time.deltaTime;
                    float evaluate = Mathf.InverseLerp(0, duration, timer);
                    _canvasGroup.alpha = _alphaCurve.Evaluate(evaluate);
                    rectTransform.localScale = Vector3.one * _scaleCurve.Evaluate(evaluate);
                    transform.FaceToCamera();
                })
                .OnComplete(() => Destroy(gameObject));
        }
    }
}