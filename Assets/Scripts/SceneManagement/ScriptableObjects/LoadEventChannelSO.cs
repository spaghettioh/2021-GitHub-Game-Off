using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Scriptable Objects/Load Event Channel",
    fileName = "LoadEventChannel")]
public class LoadEventChannelSO : ScriptableObject
{
    public UnityAction<string> OnSceneLoadRequested;

    public void Raise(string sceneName)
    {
        OnSceneLoadRequested.Invoke(sceneName);
    }
}
