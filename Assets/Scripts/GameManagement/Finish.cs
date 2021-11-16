using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class Finish : MonoBehaviour
{
    [SerializeField] private FinishEventChannelSO _finishEventChannel;

    [Header("When finished...")]
    public UnityEvent OnWin;
    public UnityEvent OnLose;

    private void OnEnable()
    {
        _finishEventChannel.OnFinished += Finished;
    }

    private void OnDisable()
    {
        _finishEventChannel.OnFinished -= Finished;
    }

    private void Finished(GameObject source)
    {
        if (source.GetComponent<WinCondition>() != null)
        {
            OnWin.Invoke();
        }
        if (source.GetComponent<LoseCondition>() != null)
        {
            OnLose.Invoke();
        }
    }
}
