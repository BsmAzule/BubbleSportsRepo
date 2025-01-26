using Events;
using FMODUnity;
using UnityEngine;

namespace Feel
{
    /// <summary>
    ///     Starts and stops an audio source in response to events.
    /// </summary>
    public class EventAudioToggle : MonoBehaviour
    {
        [SerializeField]
        private StudioEventEmitter _source;

        [SerializeField]
        private SOEvent _playEvent;

        [SerializeField]
        private SOEvent _stopEvent;

        [SerializeField]
        private bool _preventRestart;

        private void Awake()
        {
            _playEvent.AddListener(PlayAudio);
            _stopEvent.AddListener(StopAudio);
        }

        private void OnDestroy()
        {
            _source.Stop();
            _playEvent.RemoveListener(PlayAudio);
            _stopEvent.RemoveListener(StopAudio);
        }

        private void PlayAudio()
        {
            if (_preventRestart && _source.IsPlaying())
            {
                return;
            }

            _source.Play();
        }

        private void StopAudio()
        {
            _source.Stop();
        }
    }
}