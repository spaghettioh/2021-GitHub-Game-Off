using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInitializer : MonoBehaviour
{
    //[SerializeField] private LoadEventChannelSO _waxedOn;
    [SerializeField] private VoidEventChannelSO _waxOff;
    [Header("PersistentManagers")]
    [SerializeField] private string _persistentManagersSceneName = "PersistentManagers";

    private string _thisScene;

    private void Start()
    {
        _thisScene = SceneManager.GetActiveScene().name;
        if (!SceneManager.GetSceneByName(_persistentManagersSceneName).isLoaded)
        {
            Debug.Log("SceneInitializer persistent not loaded");
            // Load the managers scene and scubscribe to its complete event
            // so that the EventSystem works properly
            SceneManager.LoadSceneAsync(_persistentManagersSceneName, LoadSceneMode.Additive)
                .completed += PersistentManagersLoaded;
        }
        else
        {
            Debug.Log("SceneInitializer persistent already loaded");
            //_waxOff.Raise();
        }
    }

    private void PersistentManagersLoaded(AsyncOperation unused)
    {
        Debug.Log("SceneInitializer persistent finished loading");
        _waxOff.Raise();
    }
}
