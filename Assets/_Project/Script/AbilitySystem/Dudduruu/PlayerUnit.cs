using _Project.Script.AbilitySystem._New;
using UnityEngine;

namespace _Project.Script.AbilitySystem.Dudduruu
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerUnit : BaseUnit
    {
        public float acceleration = 20f;
        public float deceleration = 20f;

        private Vector3 velocity;
        private CharacterController _controller;

        private Vector3 moveDir;
        
        private PlayerMovement _playerMovement;
        public AbilityBaseSO abilityBaseSo;

        public override void OnAwake()
        {
            base.OnAwake();
            _playerMovement = GetComponent<PlayerMovement>();
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            // Get raw input (instant, no smoothing)
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 inputDir = new Vector3(h, 0, v).normalized;

            if (inputDir.magnitude != 0)
                moveDir = inputDir;

            // Accelerate toward target velocity
            Debug.Log(UnitStats.Speed.Value);
            Vector3 targetVelocity = inputDir * UnitStats.Speed.Value;
            velocity = Vector3.MoveTowards(velocity, targetVelocity, 
                (inputDir.magnitude > 0 ? acceleration : deceleration) * Time.deltaTime);

            // Move the player using CharacterController for no friction/stickiness
            _controller.Move(velocity * Time.deltaTime);
            
            // flip sprite depending on travel direction
            return;
            if (h > .1f) {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }

            if (h < -.1f) {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }


        public override void TakeDamage(float damageAmount)
        {
            base.TakeDamage(damageAmount);
            if (CurrentHealth <= 0)
            {
                // TODO Finish Game
                Debug.Log("Game is Over");
            }
        }

        public override Vector3 GetMousePoint()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);  // Y-up, XZ plane

            if (groundPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                return hitPoint;
            }

            return GetDirection();
        }
        public override Vector3 GetDirection() => moveDir;
        public override BaseUnit GetSelectedTarget()
        {
            // TODO Make it so you can select enemies
            return FindFirstObjectByType<DummyEnemy>();
        }
    }
}