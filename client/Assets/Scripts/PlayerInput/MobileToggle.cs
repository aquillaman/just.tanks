using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerInput
{
    public class MobileToggle : MonoBehaviour
    {
        [SerializeField] 
        private Toggle _toggle;
        public event Action<bool> OnToggle;

        private void Awake()
        {
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(bool v)
        {
            OnToggle?.Invoke(v);
        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(OnValueChanged);
        }
    }
}