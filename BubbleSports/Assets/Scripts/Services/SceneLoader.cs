using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class SceneLoader : MonoBehaviour
    {
        [Header("Event (In)")]

        [SerializeField]
        private StringEvent _loadSceneEvent;

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
            SceneManager.LoadScene(sceneName);
        }
    }
}