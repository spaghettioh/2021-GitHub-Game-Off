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
        StartCoroutine(NextGame());

        if (source.GetComponent<WinCondition>() != null)
        {
            OnWin.Invoke();
        }
        else if (source.GetComponent<LoseCondition>() != null)
        {
            OnLose.Invoke();
        }
    }

    private IEnumerator NextGame()
    {
        yield return new WaitForSeconds(2f);
        // Load another mini game
        Debug.Log($"{name} wants to load the next game.");
    }
}
