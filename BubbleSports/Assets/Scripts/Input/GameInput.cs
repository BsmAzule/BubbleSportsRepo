using Events;
using Events.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Input
{
    public class GameInput : MonoBehaviour
    {
        [Header("Event (Out)")]
        [SerializeField]
        private InputEvent _inputEvent;
        
        private bool didDrop = false;

        void Update()
        {
            if (didDrop)
            {
                return;
            }
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                _inputEvent.Raise(new InputData { Value = 1 });
                didDrop = true;
            }
        }
    }
}
