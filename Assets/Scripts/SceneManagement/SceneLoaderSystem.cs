using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderSystem : MonoBehaviour
{
    [SerializeField] private LoadEventChannelSO _loadEventChannel;
    [SerializeField] private VoidEventChannelSO _startWaxOn;
    [SerializeField] private VoidEventChannelSO _waxOnFinished;
    [SerializeField] private VoidEventChannelSO _startWaxOff;

    private string _currentActiveScene;
    private string _nextScene;

    private void OnEnable()
    {
        Debug.Log("SceneLoader enabled");
        _loadEventChannel.OnSceneLoadRequested += RequestNewScene;
    }

    private void OnDisable()
    {
        _loadEventChannel.OnSceneLoadRequested -= RequestNewScene;
    }

    private void RequestNewScene(string newScene)
    {
        Debug.Log("SceneLoader detected new scene request");
        _currentActiveScene = SceneManager.GetActiveScene().name;
        _nextScene = newScene;
        _startWaxOn.Raise();
        // Subscribe to the screen wipe finish
        _waxOnFinished.OnEventRaised += TriggerNewScene;
    }

    private void TriggerNewScene()
    {
        Debug.Log("SceneLoader detected wax on finish");
        // Unsubscribe from the screen wipe finish
        _waxOnFinished.OnEventRaised -= TriggerNewScene;
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        Debug.Log("SceneLoader loading scene");

        SceneManager.UnloadSceneAsync(_currentActiveScene);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(
            _nextScene, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("SceneLoader new scene loaded");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_nextScene));
        _currentActiveScene = _nextScene;

        _startWaxOff.Raise();
    }
}
