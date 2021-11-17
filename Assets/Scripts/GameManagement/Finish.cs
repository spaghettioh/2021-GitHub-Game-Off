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
            StartCoroutine(NextGame());
        }
        else if (source.GetComponent<LoseCondition>() != null)
        {
            OnLose.Invoke();
            StartCoroutine(NextGame());
        }
        else
        {
            Debug.LogWarning($"The mini game is finished but the thing that" +
                $"raised the event did not have a win or lose condition:" +
                $"{source.name}");
        }
    }

    private IEnumerator NextGame()
    {
        yield return new WaitForSeconds(2f);
        // Load another mini game
        Debug.Log($"{name} wants to load the next game.");
    }
}
