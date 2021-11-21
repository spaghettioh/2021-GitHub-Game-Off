using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/Void Event Channel",
    fileName = "Void_NAME")]
public class VoidEventChannelSO : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void Raise()
    {
        if (OnEventRaised != null)
        {
            Debug.Log($"{name} raised");
            OnEventRaised.Invoke();
        }
        else
        {
            Debug.LogWarning($"Void event ({name}) raised but nothing " +
                $"listens...");
        }
    }
}
