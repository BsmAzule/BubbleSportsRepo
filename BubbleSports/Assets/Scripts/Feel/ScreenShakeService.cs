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