
using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.Samples
{
    // Runtime representation of a TextClip.
    // The Serializable attribute is required to be animated by timeline, and used as a template.
    [Serializable]
    public class SplineTrackPlayableBehavior : PlayableBehaviour
    {
    }
}

