using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private LoadEventChannelSO _loadEventChannel;
    [Header("PersistentManagers")]
    [SerializeField] private string _persistentManagersSceneName = "PersistentManagers";

    private string _thisScene;

    private void Awake()
    {
        if (!SceneManager.GetSceneByName(_persistentManagersSceneName).isLoaded)
        {
            _thisScene = SceneManager.GetActiveScene().name;
            // Load the managers scene and scubscribe to its complete event
            // so that the EventSystem works properly
            SceneManager.LoadSceneAsync(_persistentManagersSceneName, LoadSceneMode.Additive)
                .completed += PersistentManagersLoaded;
            SceneManager.UnloadSceneAsync(_thisScene);
        }
    }

    private void PersistentManagersLoaded(AsyncOperation unused)
    {
        _loadEventChannel.Raise(_thisScene);
        // If you need to know when managers are done loaded, raise event here
    }
}
