using Events.Core;
using UnityEngine;
using UnityEngine.Playables;

public class FlowControl : MonoBehaviour
{
    private enum State
    {
        None,
        Intro,
        Loop,
        End
    }

    [Header("Event (In)")]

    [SerializeField]
    private InputEvent _inputEvent;

    [Header("Depend")]

    [SerializeField]
    private PlayableDirector _introDirector;

    [Tooltip("If true, the game will wait for input before starting the looped section")]
    [SerializeField]
    private bool _requireInputBeforeLoop;

    [SerializeField]
    private PlayableDirector _loopDirector;

    [Tooltip("If true, the game will wait for input before starting the ending section")]
    [SerializeField]
    private bool _requireInputBeforeEnd;

    [SerializeField]
    private PlayableDirector _endDirector;

    [Header("Debug, Readonly")]

    [SerializeField]
    private State _state = State.None;

    private bool _inputEnabled = true;

    private void Awake()
    {
        _inputEvent.AddListener(OnInput);
    }

    private void Update()
    {
        switch (_state)
        {
            case State.None:
                // Start intro
                _state = State.Intro;
                _introDirector.Play();
                break;
            case State.Intro:
                if (_introDirector != null && _introDirector.state != PlayState.Playing)
                {
                    if (!_requireInputBeforeLoop)
                    {
                        TransitionToLoop();
                    }
                }

                break;
            case State.Loop:
                if (_loopDirector != null && _loopDirector.state != PlayState.Playing)
                {
                    if (!_requireInputBeforeEnd)
                    {
                        TransitionToEnd();
                    }
                }

                break;
            case State.End:
                if (_endDirector != null && _endDirector.state != PlayState.Playing)
                {
                    // End of the game
                }

                break;
        }
    }

    private void OnDestroy()
    {
        _inputEvent.RemoveListener(OnInput);
    }

    private void OnInput()
    {
        if (!_inputEnabled)
        {
            return;
        }

        if (_state == State.Intro && _introDirector != null && _introDirector.state != PlayState.Playing)
        {
            if (_requireInputBeforeLoop)
            {
                TransitionToLoop();
            }
        }

        if (_state == State.Loop && _loopDirector != null && _loopDirector.state == PlayState.Playing)
        {
            if (_requireInputBeforeEnd)
            {
                TransitionToEnd();
            }
        }
    }

    private void TransitionToEnd()
    {
        _state = State.End;
        _endDirector.Play();
        _loopDirector.Stop();
    }

    private void TransitionToLoop()
    {
        _state = State.Loop;
        _loopDirector.Play();
        _introDirector.Stop();
    }

    public void EnableInput(bool enable)
    {
        _inputEnabled = enable;
    }
}