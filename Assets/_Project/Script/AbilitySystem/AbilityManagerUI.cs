using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _Project.Script.AbilitySystem
{
    public class AbilityManagerUI : MonoBehaviour
    {
        
        
        [Header("Ability Settings")]
        public AbilityHolderUI PrimarySkill;

        [Header("Page 1")] 
        public GameObject page1Object;
        public AbilityHolderUI skill1;
        public AbilityHolderUI skill2;
        public AbilityHolderUI skill3;

        [Header("Page 2")] 
        public GameObject page2Object;
        public AbilityHolderUI skill4;
        public AbilityHolderUI skill5;
        public AbilityHolderUI skill6;

        // Private
        private Dictionary<int, AbilityHolderUI> indexer;
        private Tween _switchSkillSetTween;

        private Sequence _currentSequence;
        private Sequence _page1Seq;
        private Sequence _page2Seq;

        private void Start()
        {
            indexer = new Dictionary<int, AbilityHolderUI>();
            indexer.Add(0, PrimarySkill);
            indexer.Add(1, skill1);
            indexer.Add(2, skill2);
            indexer.Add(3, skill3);
            indexer.Add(4, skill4);
            indexer.Add(5, skill5);
            indexer.Add(6, skill6);

            
            float delay = 0.2f;
            RectTransform page1Rect = page1Object.GetComponent<RectTransform>();
            RectTransform page2Rect = page2Object.GetComponent<RectTransform>();
            Vector3 startPos1 = page1Rect.anchoredPosition;
            Vector3 startPos2 = page2Rect.anchoredPosition;

            _page1Seq = DOTween.Sequence();
            _page1Seq.Join(page1Object.GetComponent<CanvasGroup>().DOFade(1, delay));
            _page1Seq.Join(page1Rect.DOAnchorPos(startPos1 + Vector3.up * 100, 0)); // Jump instantly up
            _page1Seq.Join(page1Rect.DOAnchorPos(startPos1, delay));                 // Animate back to start
            _page1Seq.Join(page2Object.GetComponent<CanvasGroup>().DOFade(0, delay));
            _page1Seq.Join(page2Rect.DOAnchorPos(startPos2 - Vector3.up * 100, delay));
            _page1Seq.SetAutoKill(false);
            // _page1Seq.Pause();

            _page2Seq = DOTween.Sequence();
            _page2Seq.Join(page2Object.GetComponent<CanvasGroup>().DOFade(1, delay));
            _page2Seq.Join(page2Rect.DOAnchorPos(startPos2 + Vector3.up * 100, 0));
            _page2Seq.Join(page2Rect.DOAnchorPos(startPos2, delay));
            _page2Seq.Join(page1Object.GetComponent<CanvasGroup>().DOFade(0, delay));
            _page2Seq.Join(page1Rect.DOAnchorPos(startPos1 - Vector3.up * 100, delay));
            _page2Seq.SetAutoKill(false);
            _page2Seq.Pause();
        }

        private void Update()
        {
            TryUseAbility(KeyCode.Mouse0, PrimarySkill);

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                _currentSequence?.Complete();
                _currentSequence = _page2Seq;
                _currentSequence.Restart();
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                _currentSequence?.Complete();
                _currentSequence = _page1Seq;
                _currentSequence.Restart();
            }
            
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                TryUseAbility(KeyCode.Q, skill1);
                TryUseAbility(KeyCode.E, skill2);
                TryUseAbility(KeyCode.R, skill3);
            }
            else
            {
                _switchSkillSetTween?.Kill();
                TryUseAbility(KeyCode.Q, skill4);
                TryUseAbility(KeyCode.E, skill5);
                TryUseAbility(KeyCode.R, skill6);
            }
        }
        
        private void TryUseAbility(KeyCode keyCode, AbilityHolderUI skillUI)
        {
            Ability ability = skillUI.GetAbility;
            if (ability == null) return;
            if (!ability.enabled) return;
            if (!ability.CanUse()) return;
            
            if (Input.GetKeyDown(keyCode))
                ability.OnButtonDown();
            else if (Input.GetKey(keyCode))
                ability.OnButtonHeld();
            else if (Input.GetKeyUp(keyCode))
                ability.OnButtonUp();
        }

        public void AssignAbility(int index, Ability ability)
        {
            indexer[index].Bind(ability);
        }
    }
}