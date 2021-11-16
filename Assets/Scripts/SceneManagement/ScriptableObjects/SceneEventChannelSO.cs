using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Scriptable Objects/Scene Event Channel",
    fileName = "SceneEventChannel")]
public class SceneEventChannelSO : ScriptableObject
{
    public UnityAction<string> OnSceneLoadRequested;

    public void LoadScene(string sceneName)
    {
        OnSceneLoadRequested.Invoke(sceneName);
    }
}
