﻿using UnityEngine;
using UnityEngine.Events;

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
