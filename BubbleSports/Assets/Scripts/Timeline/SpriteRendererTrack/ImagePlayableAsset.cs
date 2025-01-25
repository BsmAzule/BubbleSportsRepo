using System;
using Timeline.Samples;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

// Represents the serialized data for a clip on the TextTrack
[Serializable]
public class ImagePlayableAsset : PlayableAsset, ITimelineClipAsset
{
    [NoFoldOut]
    [NotKeyable] // NotKeyable used to prevent Timeline from making fields available for animation.
    public ImagePlayableBehavior template = new();

    // Implementation of ITimelineClipAsset. This specifies the capabilities of this timeline clip inside the editor.
    public ClipCaps clipCaps => ClipCaps.Blending;

    // Creates the playable that represents the instance of this clip.
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        // Using a template will clone the serialized values
        return ScriptPlayable<ImagePlayableBehavior>.Create(graph, template);
    }
}