using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _startingTime = 5f;
    [Space]
    [SerializeField] private Animator _timerProgress;
    [SerializeField] private Animator _timerNumbers;
    [Space]
    [SerializeField] private FinishEventChannelSO _finishEventChannel;

    private float _timeRemaining;

    private void Start()
    {
        // Set the timer to default config
        _timeRemaining = _startingTime;
    }

    private void Update()
    {
        if (!MiniGameFinish.InteractionsDisabled)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
                _timerNumbers.SetFloat("TimeRemaining",
                    Mathf.Ceil(_timeRemaining));
            }
            // Time is up
            else if (_timeRemaining <= 0)
            {
                _finishEventChannel.OnFinished(gameObject);
                _timerProgress.gameObject.SetActive(false);
            }
        }

        if (MiniGameFinish.InteractionsDisabled)
        {
            _timerProgress.enabled = false;
        }
    }
}
