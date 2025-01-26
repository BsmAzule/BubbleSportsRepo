using Events;
using Events.Core;
using UnityEngine;
using WiimoteApi;

public class WiimoteInput : MonoBehaviour
{
    [Header("Config")]

    [SerializeField]
    private Vector2 _accelRange;

    [SerializeField]
    private float _accelTriggerCooldown;

    [Header("Events (In)")]

    [SerializeField]
    private FloatEvent _rumbleEvent;

    [Header("Events (Out)")]

    [SerializeField]
    private InputEvent _AInputEvent;

    [SerializeField]
    private InputEvent _accelInputEvent;

    [SerializeField]
    private VoidEvent _dpadUpEvent;

    [SerializeField]
    private VoidEvent _dpadDownEvent;

    private Wiimote _wiimote;

    private float _rumbleDuration;

    private bool _dPadUp;
    private bool _dPadDown;
    private bool _aButton;

    private float _accelTriggerCooldownTimer;

    private void Awake()
    {
        InitWiimotes();
        _rumbleEvent.AddListener(HandleRumbleEvent);
    }

    private void Update()
    {
        if (_wiimote == null)
        {
            return;
        }

        if (_rumbleDuration > 0f)
        {
            _rumbleDuration -= Time.deltaTime;
            if (_rumbleDuration <= 0f)
            {
                _wiimote.RumbleOn = false;
                _wiimote.SendStatusInfoRequest();
            }
        }

        _accelTriggerCooldownTimer -= Time.deltaTime;

        int ret;
        do
        {
            ret = _wiimote.ReadWiimoteData();
            if (_wiimote.Button.a && !_aButton)
            {
                _AInputEvent.Raise(new InputData { Value = 1 });
            }

            _aButton = _wiimote.Button.a;

            if (_wiimote.Button.d_up && !_dPadUp)
            {
                _dpadUpEvent.Raise();
            }

            _dPadUp = _wiimote.Button.d_up;

            if (_wiimote.Button.d_down && !_dPadDown)
            {
                _dpadDownEvent.Raise();
            }

            _dPadDown = _wiimote.Button.d_down;

            float[] accelData = _wiimote.Accel.GetCalibratedAccelData();
            var accelVector = new Vector3(accelData[0], accelData[1], accelData[2]);
            // Debug.Log($"Accel: {accelVector}, magnitude: {accelVector.magnitude}");

            float accelNormalized = Mathf.InverseLerp(_accelRange.x, _accelRange.y, accelVector.magnitude);

            if (accelNormalized > 0f && _accelTriggerCooldownTimer <= 0f)
            {
                _accelTriggerCooldownTimer = _accelTriggerCooldown;
                _accelInputEvent.Raise(new InputData { Value = accelNormalized });
            }
        } while (ret > 0);
    }

    private void OnDestroy()
    {
        FinishedWithWiimotes();
        _rumbleEvent.RemoveListener(HandleRumbleEvent);
    }

    private void HandleRumbleEvent(float value)
    {
        if (_wiimote != null)
        {
            _wiimote.RumbleOn = value > 0f;
            _wiimote.SendStatusInfoRequest();
            _rumbleDuration = Mathf.Max(_rumbleDuration, value);
        }
    }

    private void InitWiimotes()
    {
        WiimoteManager.FindWiimotes(); // Poll native bluetooth drivers to find Wiimotes
        Debug.Log($"Found {WiimoteManager.Wiimotes.Count} wiimotes");
        foreach (Wiimote remote in WiimoteManager.Wiimotes)
        {
            _wiimote = remote;
            _wiimote.Accel.CalibrateAccel(AccelCalibrationStep.A_BUTTON_UP);
            remote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL);
            remote.SendPlayerLED(true, false, false, false);
            break;
            // Do stuff.
        }
    }

    private void FinishedWithWiimotes()
    {
        if (_wiimote != null)
        {
            WiimoteManager.Cleanup(_wiimote);
        }
    }
}