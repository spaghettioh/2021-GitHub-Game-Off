using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
