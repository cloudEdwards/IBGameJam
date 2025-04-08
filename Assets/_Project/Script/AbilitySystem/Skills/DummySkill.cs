using UnityEngine;

namespace _Project.Script.AbilitySystem
{
    public class DummySkill : Ability
    {
        public override void OnUpdate(float deltaTime)
        {
        }

        public override void OnButtonDown()
        {
            StartCooldown();
            Debug.Log("Button Down");
        }

        public override void OnButtonHeld()
        {
            Debug.Log("Button Held");
        }

        public override void OnButtonUp()
        {
            Debug.Log("Button Up");
        }
    }
}