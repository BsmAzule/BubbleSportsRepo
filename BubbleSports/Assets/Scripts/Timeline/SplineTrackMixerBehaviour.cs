using Timeline.Samples;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

namespace Timeline
{
    public class SplineTrackMixerBehavior : PlayableBehaviour
    {
        CinemachineSplineDolly _splineDolly;

        // Called every frame that the timeline is evaluated. ProcessFrame is invoked after its' inputs.
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            _splineDolly = playerData as CinemachineSplineDolly;

            if (_splineDolly == null)
                return;
            
            int inputCount = playable.GetInputCount();

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                ScriptPlayable<SplineTrackPlayableBehavior> inputPlayable = (ScriptPlayable<SplineTrackPlayableBehavior>)playable.GetInput(i);
                SplineTrackPlayableBehavior input = inputPlayable.GetBehaviour();
                
                var normalizedInputTime = (float)(inputPlayable.GetTime() / inputPlayable.GetDuration());

                Debug.Log(normalizedInputTime);
                _splineDolly.CameraPosition = normalizedInputTime;
            }
        }
    }
}

