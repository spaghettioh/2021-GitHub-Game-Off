using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderSystem : MonoBehaviour
{
    [SerializeField] private SceneEventChannelSO _sceneEventChannel;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void OnEnable()
    {
        _sceneEventChannel.OnSceneLoadRequested += LoadAScene;
    }

    private void OnDisable()
    {
        _sceneEventChannel.OnSceneLoadRequested -= LoadAScene;
    }

    /// <summary>
    /// Allow the scene manager to persist between scenes, but don't create more
    /// than one instance
    /// </summary>
    private void SetUpSingleton()
    {
        SceneLoaderSystem[] sceneManagers =
            FindObjectsOfType<SceneLoaderSystem>();
        if (sceneManagers.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    public void LoadAScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
