using UnityEngine;

namespace PlayerInput
{
    public class MovementController : MonoBehaviour
    {
        public Rigidbody rb;
        public Quaternion targetDirection;
        public float targetSpeed;

        public float speedInterpolationStrength;
        public float rotationInterpolationStrength;

        private void FixedUpdate()
        {
            Quaternion nextRotation;
            if (Mathf.Abs(targetSpeed) > 0.01f)
            {
                float angularVelSpeed = rotationInterpolationStrength * Quaternion.Angle(rb.rotation, targetDirection) /
                    180f * Time.fixedDeltaTime;
                nextRotation = Quaternion.RotateTowards(rb.rotation, targetDirection, angularVelSpeed);
            }
            else
            {
                nextRotation = rb.rotation;
            }

            Vector3 velocityInDirection = Vector3.Project(rb.velocity, rb.rotation * Vector3.forward);
            float speedError = (targetSpeed - velocityInDirection.magnitude);
            float accUpdate = speedError * speedInterpolationStrength * Time.fixedDeltaTime;
            Debug.Log($"speedError: {speedError}");
            Debug.Log($"accUpdate: {accUpdate}");

            Vector3 velocityUpdate = -velocityInDirection;
            velocityUpdate += (nextRotation * Vector3.forward) * (velocityInDirection.magnitude * accUpdate);
            rb.AddForce(velocityUpdate, ForceMode.VelocityChange);
        
            // Debug.Log($"velocityUpdate: {velocityUpdate}");

            rb.MoveRotation(nextRotation);
            rb.angularVelocity = Vector3.zero;
        }
    }
}