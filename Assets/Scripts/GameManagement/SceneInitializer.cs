using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private LoadEventChannelSO _loadEventChannel;
    [Header("PersistentManagers")]
    [SerializeField] private string _sceneName = "PersistentManagers";

    private void Awake()
    {
        if (!SceneManager.GetSceneByName(_sceneName).isLoaded)
        {
            Debug.Log("Loading persistent managers");
            // Load the managers scene and scubscribe to its complete event
            SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive)
                .completed += PersistentManagersLoaded;
        }
    }

    private void PersistentManagersLoaded(AsyncOperation unused)
    {
        //_loadEventChannel.Raise(_thisScene.name);
        // If you need to know when managers are done loaded, raise event here
    }
}
