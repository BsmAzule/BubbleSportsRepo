using Events;
using Events.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class WiimoteNavigationHook : MonoBehaviour
    {
        [SerializeField]
        private VoidEvent _dpadUpEvent;

        [SerializeField]
        private VoidEvent _dpadDownEvent;

        [SerializeField]
        private InputEvent _aPressedEvent;

        private void Awake()
        {
            _dpadUpEvent.AddListener(HandleDpadUpEvent);
            _dpadDownEvent.AddListener(HandleDpadDownEvent);
            _aPressedEvent.AddListener(HandleAPressedEvent);
        }

        private void OnDestroy()
        {
            _dpadUpEvent.RemoveListener(HandleDpadUpEvent);
            _dpadDownEvent.RemoveListener(HandleDpadDownEvent);
            _aPressedEvent.RemoveListener(HandleAPressedEvent);
        }

        private void HandleDpadUpEvent()
        {
            Move(MoveDirection.Up);
        }

        private void HandleDpadDownEvent()
        {
            Move(MoveDirection.Down);
        }

        private void HandleAPressedEvent()
        {
            if (EventSystem.current == null || EventSystem.current.currentSelectedGameObject == null)
            {
                return;
            }

            ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new BaseEventData(EventSystem.current),
                ExecuteEvents.submitHandler);
        }

        private void Move(MoveDirection direction)
        {
            if (EventSystem.current == null || EventSystem.current.currentSelectedGameObject == null)
            {
                return;
            }

            var data = new AxisEventData(EventSystem.current);

            data.moveDir = direction;

            data.selectedObject = EventSystem.current.currentSelectedGameObject;

            ExecuteEvents.Execute(data.selectedObject, data, ExecuteEvents.moveHandler);
        }
    }
}