using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Script.AbilitySystem
{
    public class AbilityManagerUI : MonoBehaviour
    {
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


        private Dictionary<int, AbilityHolderUI> indexer;

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
        }

        private void Update()
        {
            TryUseAbility(KeyCode.Mouse0, PrimarySkill);

            if (!Input.GetKey(KeyCode.LeftControl))
            {
                TryUseAbility(KeyCode.Q, skill1);
                TryUseAbility(KeyCode.E, skill2);
                TryUseAbility(KeyCode.R, skill3);
            }
            else
            {
                TryUseAbility(KeyCode.Q, skill4);
                TryUseAbility(KeyCode.E, skill5);
                TryUseAbility(KeyCode.R, skill6);
            }
        }
        
        private void TryUseAbility(KeyCode keyCode, AbilityHolderUI skillUI)
        {
            Ability ability = skillUI.GetAbility;
            if (ability == null) return;
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