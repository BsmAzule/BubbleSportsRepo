using UnityEngine;

namespace Core
{
    public class ServiceContainer : MonoBehaviour
    {
        public static ServiceContainer Instance { get; private set; }
    
        [SerializeField]
        private GameObject _servicePrefab;
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Instantiate(_servicePrefab, transform);
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    
        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}
