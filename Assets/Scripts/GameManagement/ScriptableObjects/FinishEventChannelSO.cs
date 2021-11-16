using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Objects/Finish Event Channel",
    fileName = "FinishEventChannel")]
public class FinishEventChannelSO : ScriptableObject
{
    public UnityAction<GameObject> OnFinished;

    public void Raise(GameObject source)
    {
        if (OnFinished != null)
        {
            OnFinished.Invoke(source);
        }
        else
        {
            Debug.LogWarning("Finish event raised but nothing listens...");
        }
    }
}
