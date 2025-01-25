<<<<<<< HEAD
=======
using System;
using Events.Core;
>>>>>>> origin/main
using Unity.Cinemachine;
using UnityEngine;

namespace Feel
{
    /// <summary>
    ///     Service for shaking the screen.
    /// </summary>
    public class ScreenShakeService : MonoBehaviour
    {
        [SerializeField]
        private CinemachineImpulseSource _impulseSource;

<<<<<<< HEAD
        public static ScreenShakeService Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
=======
        [Header("Event (In)")]
        [SerializeField]
        private ScreenShakeEvent _screenShakeEvent;

        private void Awake()
        {
            _screenShakeEvent.AddListener(ShakeScreen);
        }
        
        private void OnDestroy()
        {
            _screenShakeEvent.RemoveListener(ShakeScreen);
        }

        private void Update()
        {
            if (Camera.main == null)
            {
                return;
            }
            transform.position = Camera.main.transform.position;
>>>>>>> origin/main
        }

        public void ShakeScreen(float duration, Vector3 velocity, CinemachineImpulseDefinition.ImpulseShapes shapes)
        {
            _impulseSource.ImpulseDefinition.TimeEnvelope.SustainTime = duration;
            _impulseSource.ImpulseDefinition.ImpulseShape = shapes;
            _impulseSource.GenerateImpulseWithVelocity(velocity);
        }

        public void ShakeScreen(ScreenShakeSO screenShakeSO)
        {
            ShakeScreen(screenShakeSO.Duration, screenShakeSO.ScaledVelocity, screenShakeSO.Shapes);
        }
    }
}