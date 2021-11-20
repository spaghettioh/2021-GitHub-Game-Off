using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderSystem : MonoBehaviour
{
    [SerializeField] private LoadEventChannelSO _loadEventChannel;

    private Scene _currentActiveScene;

    private void Awake()
    {
        _currentActiveScene = SceneManager.GetActiveScene();
    }

    private void OnEnable()
    {
        _loadEventChannel.OnSceneLoadRequested += RequestNewScene;
    }

    private void OnDisable()
    {
        _loadEventChannel.OnSceneLoadRequested -= RequestNewScene;
    }

    private void RequestNewScene(string newScene)
    {
        StartCoroutine(LoadScene(newScene));
    }

    private IEnumerator LoadScene(string newScene)
    {
        Scene activeScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(
            newScene, LoadSceneMode.Additive);
        Debug.Log("AsyncLoad kicking off");

        while (!asyncLoad.isDone)
        {
            Debug.Log("Loading...");
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(newScene));
        SceneManager.UnloadSceneAsync(activeScene);
    }
}
