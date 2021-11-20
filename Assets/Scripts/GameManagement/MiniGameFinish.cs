using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MiniGameFinish : MonoBehaviour
{
    [Header("When finished...")]
    [SerializeField] private string _nextScene;
    public UnityEvent OnWin;
    public UnityEvent OnLose;
    [Space]

    [Header("// Prefab stuff")]
    private static bool _miniGameIsFinished = false;
    // Used by other scripts to know if they should keep working
    public static bool MiniGameIsFinished
    {
        get { return _miniGameIsFinished; }
        private set { _miniGameIsFinished = value; }
    }

    [Header("OnWin...")]
    [SerializeField] private SpriteRenderer _winBackground;
    [SerializeField] private AudioCueSO _winSound;

    [Header("OnLose...")]
    [SerializeField] private SpriteRenderer _loseBackground;
    [SerializeField] private AudioCueSO _loseSound;

    [Header("Listening to...")]
    [SerializeField] private FinishEventChannelSO _finishEventChannel;

    [Header("Broadcasting to...")]
    [SerializeField] private LoadEventChannelSO _loadEventchannel;
    [SerializeField] private AudioEventChannelSO _audioEventChannel;

    private void OnEnable()
    {
        _finishEventChannel.OnFinished += Finished;
    }

    private void Awake()
    {
        MiniGameIsFinished = false;
    }

    private void OnDisable()
    {
        _finishEventChannel.OnFinished -= Finished;
    }

    private void Finished(GameObject source)
    {
        MiniGameIsFinished = true;
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
        _winBackground.gameObject.SetActive(true);
        _audioEventChannel.Raise(_winSound);
        OnWin.Invoke();
    }

    /// <summary>
    /// Enable background & play sound, and invoke inspector events
    /// </summary>
    private void Lose()
    {
        _loseBackground.gameObject.SetActive(true);
        _audioEventChannel.Raise(_loseSound);
        OnLose.Invoke();

    }

    /// <summary>
    /// Waits a few seconds a to load another mini game
    /// </summary>
    /// <returns></returns>
    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(2f);
        _loadEventchannel.Raise(_nextScene);
    }
}
