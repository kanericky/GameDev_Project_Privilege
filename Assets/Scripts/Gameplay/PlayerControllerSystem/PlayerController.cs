using System;
using UnityEngine;

namespace Gameplay.PlayerControllerSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Motor Settings")] 
        [SerializeField] private float driftFactor = 0.95f;
        [SerializeField] private float accelerationFactor = 30.0f;
        [SerializeField] private float turnFactor = 3.5f;
        [SerializeField] private float maxSpeed = 20f;
        
        
        [Header("Debug")]
        [SerializeField] private float accelerationInput = 0;
        [SerializeField] private float steeringInput = 0;

        [SerializeField] private float rotationAngle = 0;
        [SerializeField] private float velocityVsUp = 0;

        private Rigidbody2D _motorRigidbody2D;

        private void Awake()
        {
            _motorRigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            ApplyEngineForce();
            KillOrthogonalVelocity();
            ApplySteering();
        }
        
        void ApplyEngineForce()
        {
            
            // ------------------------------------------ //
            //             HANDLE MAX SPEED               //
            // ------------------------------------------ //
            velocityVsUp = Vector2.Dot(transform.up, _motorRigidbody2D.velocity);

            // The motor has reached the max speed when moving forward
            if (velocityVsUp > maxSpeed && accelerationInput > 0) return;
            
            // The motor has reached the max speed when moving backward
            if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0) return;

            // Limit so the motor cannot go faster in any direction while accelerating
            if (_motorRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0) return;
            
            // ------------------------------------------ //
            //           HANDLE DE-ACCELERATE             //
            // ------------------------------------------ //
            
            // If player release the acceleration button, increase the drag to stop the motor
            if (accelerationInput == 0)
            {
                _motorRigidbody2D.drag = Mathf.Lerp(_motorRigidbody2D.drag, 10.0f, Time.fixedDeltaTime);
            }
            else
            {
                _motorRigidbody2D.drag = 0;
            }

            // Create engine force
            Vector2 engineForceVector = transform.up * (accelerationInput * accelerationFactor);

            // Apply engine force
            _motorRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
        }

        void ApplySteering()
        {
            float minSpeedBeforeAllowTurn = (_motorRigidbody2D.velocity.magnitude / 8);
            minSpeedBeforeAllowTurn = Mathf.Clamp01(minSpeedBeforeAllowTurn);
            
            // Update angle
            if (velocityVsUp > 0) rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurn;
            else rotationAngle += steeringInput * turnFactor * minSpeedBeforeAllowTurn;

            // Apply angle
            _motorRigidbody2D.MoveRotation(rotationAngle);
        }

        void KillOrthogonalVelocity()
        {
            Vector2 forwardVelocity = transform.up * Vector2.Dot(_motorRigidbody2D.velocity, transform.up);
            Vector2 rightVelocity = transform.right * Vector2.Dot(_motorRigidbody2D.velocity, transform.right);

            _motorRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
        }

        public void SetInputVector(Vector2 inputVector)
        {
            steeringInput = inputVector.x;
            accelerationInput = inputVector.y;
        }
    }
}
