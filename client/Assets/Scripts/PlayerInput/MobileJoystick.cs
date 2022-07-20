using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerInput
{
    public class MobileJoystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
    {
        private RectTransform _joystickRectTransform;

        [SerializeField] private float _dragThreshold = 0.6f;
        [SerializeField] private int _dragMovementDistance = 30;
        [SerializeField] private int _dragOffsetDistance = 100;

        public event Action<Vector2> OnMove;

        private void Awake()
        {
            _joystickRectTransform = GetComponent<RectTransform>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // no op
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _joystickRectTransform.anchoredPosition = Vector2.zero;
            OnMove?.Invoke(Vector2.zero);
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _joystickRectTransform,
                eventData.position,
                null, out var offset);
            offset = Vector2.ClampMagnitude(offset, _dragOffsetDistance) / _dragOffsetDistance;
            _joystickRectTransform.anchoredPosition = offset * _dragOffsetDistance;
            Vector2 inputVector = CalculateInputVector(offset);
            OnMove?.Invoke(inputVector);
        }

        private Vector2 CalculateInputVector(Vector2 offset)
        {
            float x = Math.Abs(offset.x) > _dragThreshold ? offset.x : 0;
            float y = Math.Abs(offset.y) > _dragThreshold ? offset.y : 0;
            return new Vector2(x, y);
        }
    }
}