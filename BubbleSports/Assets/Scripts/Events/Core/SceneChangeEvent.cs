using System;
using Events;
using UnityEngine;
using UnityEngine.Timeline;

[Serializable]
public struct StringChangeData
{
    public string SceneName;
    public TimelineAsset TransitionOutOfScene;
    public TimelineAsset TransitionIntoScene;
}

[CreateAssetMenu(menuName = "Events/Scene Change Event")]
public class SceneChangeEvent : SOEvent<StringChangeData>
{
}