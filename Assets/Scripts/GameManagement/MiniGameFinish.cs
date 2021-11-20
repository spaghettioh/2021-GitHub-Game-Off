using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameFinish : MonoBehaviour
{
    [SerializeField] private FinishEventChannelSO _finishEventChannel;

    [Header("When finished...")]
    public UnityEvent OnWin;
    public UnityEvent OnLose;

    private static bool _miniGameIsFinished = false;
    // Used by other scripts to know if they should keep working
    public static bool MiniGameIsFinished {
        get { return _miniGameIsFinished; }
        private set { _miniGameIsFinished = value; }
    }

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
        _miniGameIsFinished = true;

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

    /// <summary>
    /// Waits a few seconds a to load another mini game
    /// </summary>
    /// <returns></returns>
    private IEnumerator NextGame()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log($"{name} wants to load the next game.");
    }
}
