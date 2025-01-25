using System;
using Timeline.Samples;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Timeline
{
    // Represents the serialized data for a clip on the TextTrack
    [Serializable]
    public class SplineTrackPlayableAsset : PlayableAsset, ITimelineClipAsset
    {
        [NoFoldOut]
        [NotKeyable] // NotKeyable used to prevent Timeline from making fields available for animation.
        public SplineTrackPlayableBehavior template = new SplineTrackPlayableBehavior();

        public ClipCaps clipCaps
        {
            get { return ClipCaps.None; }
        }

        // Creates the playable that represents the instance of this clip.
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            // Using a template will clone the serialized values
            return ScriptPlayable<SplineTrackPlayableBehavior>.Create(graph, template);
        }
    }
}

