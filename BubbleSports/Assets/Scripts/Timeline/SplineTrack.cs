using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Timeline
{
    // A track that allows the user to change Text parameters from a Timeline.
    // It demonstrates the following
    //  * How to support blending of timeline clips.
    //  * How to change data over time on Components that is not supported by Animation.
    //  * Putting properties into preview mode.
    //  * Reacting to changes on the clip from the Timeline Editor.
    // Note: This track requires the TextMeshPro package to be installed in the project.
    [TrackColor(0.1394896f, 0.4411765f, 0.3413077f)]
    [TrackClipType(typeof(SplineTrackPlayableAsset))]
    [TrackBindingType(typeof(CinemachineSplineDolly))]
    public class SplineTrack : TrackAsset
    {
        // Creates a runtime instance of the track, represented by a PlayableBehaviour.
        // The runtime instance performs mixing on the timeline clips.
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<SplineTrackMixerBehavior>.Create(graph, inputCount);
        }

        // Invoked by the timeline editor to put properties into preview mode. This permits the timeline
        // to temporarily change fields for the purpose of previewing in EditMode.
        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
            CinemachineSplineDolly trackBinding = director.GetGenericBinding(this) as CinemachineSplineDolly;
            if (trackBinding == null)
                return;
            
            driver.AddFromName<CinemachineSplineDolly>(trackBinding.gameObject, "m_SplineSettings.Position");

            base.GatherProperties(director, driver);
        }
    }
}

