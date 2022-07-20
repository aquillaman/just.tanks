using UnityEngine;

namespace PlayerInput
{
    public class PlayerController : MonoBehaviour
    {
        private IAgentInput _input;
        private IAgent _agent;

        public void SetController(IAgentInput input)
        {
            _input = input;

            _input.OnMovement += InputOnMovement;
            _input.OnWeaponChange += InputOnWeaponChange;
        }

        private void Awake()
        {
            _agent = GetComponent<IAgent>();
        }

        private void InputOnWeaponChange()
        {
            _agent.ChangeWeapon();
        }

        private void InputOnMovement(Vector2 value)
        {
            _agent.Move(value);
        }

        private void OnDestroy()
        {
            if (_input != null)
            {
                _input.OnMovement -= InputOnMovement;
                _input.OnWeaponChange -= InputOnWeaponChange;
            }

            _input = null;
            _agent = null;
        }
    }
}