using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderSystem : MonoBehaviour
{
    [SerializeField] private LoadEventChannelSO _loadEventChannel;
    [SerializeField] private Animator _screenWipeAnimator;
    [SerializeField] private float _screenWipeDuration;

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
        _screenWipeAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(_screenWipeDuration);
        
        SceneManager.UnloadSceneAsync(_currentActiveScene);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(
            newScene, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(newScene));
        _currentActiveScene = SceneManager.GetActiveScene();
        _screenWipeAnimator.SetTrigger("End");
    }
}
