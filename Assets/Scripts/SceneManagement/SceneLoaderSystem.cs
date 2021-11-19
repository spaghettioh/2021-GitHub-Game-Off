using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderSystem : MonoBehaviour
{
    [SerializeField] private SceneEventChannelSO _sceneEventChannel;

    private void OnEnable()
    {
        _sceneEventChannel.OnSceneLoadRequested += LoadAScene;
    }

    private void OnDisable()
    {
        _sceneEventChannel.OnSceneLoadRequested -= LoadAScene;
    }

    public void LoadAScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
