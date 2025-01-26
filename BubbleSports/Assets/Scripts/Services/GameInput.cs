using Events.Core;
using UnityEngine;

namespace Input
{
    public class GameInput : MonoBehaviour
    {
        [Header("Event (Out)")]

        [SerializeField]
        private InputEvent _inputEvent;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                _inputEvent.Raise(new InputData { Value = 1 });
            }
        }
    }
}