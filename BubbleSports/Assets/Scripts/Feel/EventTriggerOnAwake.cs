using Events;
using UnityEngine;

namespace Feel
{
    public class EventTriggerOnAwake : MonoBehaviour
    {
        [SerializeField]
        private VoidEvent _event;

        private void Start()
        {
            _event.Raise();
        }
    }
}