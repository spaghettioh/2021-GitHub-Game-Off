using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _startingTime = 5f;
    [Space]
    [SerializeField] private TMP_Text _timeRemainingText;
    [SerializeField] private Image _timerProgress;
    [Space]
    [SerializeField] private FinishEventChannelSO _finishEventChannel;
    [SerializeField] private LoadEventChannelSO _screenWiped;

    private float _timerProgressActual;
    private float _timeRemaining;

    private void OnEnable()
    {
        //_screenWiped.OnSceneLoadRequested += DisableTimer;
    }

    private void Start()
    {
        // Set the timer to default config
        _timeRemainingText.text = _startingTime.ToString();
        _timeRemaining = _startingTime;
        _timerProgress.fillAmount = 1f;
    }

    private void Update()
    {
        if (!MiniGameFinish.MiniGameIsFinished)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
                _timeRemainingText.text = Mathf.Ceil(_timeRemaining).ToString();

                _timerProgressActual -= Time.deltaTime;

                // Need to check because setting the radial less than 0 breaks
                if (_timerProgressActual <= 0f)
                {
                    _timerProgressActual = 1f;
                }
            }
            // Time is up
            else if (_timeRemaining <= 0)
            {
                _finishEventChannel.OnFinished(gameObject);
                _timeRemaining = 0;
                _timerProgressActual = 0;
            }

            // Change the progress of the timer radial
            _timerProgress.fillAmount = _timerProgressActual;
        }
    }

    private void DisableTimer(string unused)
    {
        gameObject.SetActive(false);
    }
}

