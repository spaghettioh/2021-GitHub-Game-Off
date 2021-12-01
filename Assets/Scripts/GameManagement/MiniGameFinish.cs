using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameFinish : MonoBehaviour
{
    [Header("When finished...")]
    [SerializeField] private string _nextScene;
    [Space]
    public UnityEvent OnWin;
    public UnityEvent OnLose;

    [Header("Listening to...")]
    [SerializeField] private FinishEventChannelSO _finishEventChannel;

    [Header("Broadcasting to...")]
    [SerializeField] private LoadEventChannelSO _loadEventchannel;

    private static bool _miniGameIsFinished = false;
    // Used by other scripts to know if they should keep working
    public static bool InteractionsDisabled
    {
        get { return _miniGameIsFinished; }
        private set { _miniGameIsFinished = value; }
    }

    private void Awake()
    {
        InteractionsDisabled = false;
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
        InteractionsDisabled = true;
        StartCoroutine(NextScene());

        if (source.GetComponent<WinCondition>() != null)
        {
            Win();
        }
        else if (source.GetComponent<LoseCondition>() != null)
        {
            Lose();
        }
        else
        {
            Debug.LogWarning($"The mini game is finished but the thing that" +
                $"raised the event did not have a Win/LoseCondition():\n" +
                $"{source.name}");
        }
    }

    /// <summary>
    /// Enable background & play sound, and invoke inspector events
    /// </summary>
    private void Win()
    {
        OnWin.Invoke();
    }

    /// <summary>
    /// Enable background & play sound, and invoke inspector events
    /// </summary>
    private void Lose()
    {
        OnLose.Invoke();
    }

    /// <summary>
    /// Waits a few seconds a to load another mini game
    /// </summary>
    /// <returns></returns>
    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(2f);
        _loadEventchannel?.Raise(_nextScene);
    }
}
