using System.Collections;
using Events;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

namespace Services
{
    public class SceneLoader : MonoBehaviour
    {
        [Header("Event (In)")]

        [SerializeField]
        private StringEvent _loadSceneEvent;

        [Header("Transitions")]

        [SerializeField]
        private PlayableDirector _playableDirector;

        [SerializeField]
        private TimelineAsset _transitionOutOfScene;

        [SerializeField]
        private TimelineAsset _transitionIntoScene;

        private void OnEnable()
        {
            _loadSceneEvent.AddListener(LoadScene);
        }

        private void OnDisable()
        {
            _loadSceneEvent.RemoveListener(LoadScene);
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneWithTransition(sceneName));
        }

        private IEnumerator LoadSceneWithTransition(string sceneName)
        {
            _playableDirector.playableAsset = _transitionOutOfScene;
            _playableDirector.Play();

            yield return new WaitForSeconds((float)_transitionOutOfScene.duration);

            SceneManager.LoadScene(sceneName);

            _playableDirector.playableAsset = _transitionIntoScene;
            _playableDirector.Play();
        }
    }
}