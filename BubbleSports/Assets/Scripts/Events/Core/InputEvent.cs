using UnityEngine;

namespace Events.Core
{
    public struct InputData
    {
        public float Value;

        public override string ToString()
        {
            return $"Value: {Value}";
        }
    }

    [CreateAssetMenu(fileName = "InputEvent", menuName = "Events/InputEvent")]
    public class InputEvent : SOEvent<InputData>
    {
    }
}