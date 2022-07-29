using UnityEngine;

namespace PlayerInput
{
    public class PlayerMovementController : MonoBehaviour
    {
        public float Velocity = 0.5f;
        public float AngularVelocity = 0.5f;
        private Vector2 _input;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector2 input)
        {
            _input = input;
        }

        public void FixedUpdate()
        {
            if (_input == Vector2.zero) return;
            var force = (Vector3.forward * _input.y) + (Vector3.right * _input.x);
            _rigidbody.AddForce(force * Velocity - _rigidbody.velocity, ForceMode.VelocityChange);
            _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, Quaternion.LookRotation(force), Time.fixedDeltaTime * AngularVelocity);
        }
    }
}