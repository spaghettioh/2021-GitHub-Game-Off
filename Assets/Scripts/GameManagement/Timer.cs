using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _startingTime = 5f;
    [Space, SerializeField] private TMP_Text _timeRemainingText;
    [SerializeField] private Image _timerProgress;
    [Space, SerializeField] private FinishEventChannelSO _finishEventChannel;

    private float _timerProgressActual;
    private float _timeRemaining;
    private bool _gameIsActive = true;

    private void OnEnable()
    {
        _finishEventChannel.OnFinished += StopTimer;
    }

    private void OnDisable()
    {
        _finishEventChannel.OnFinished -= StopTimer;
    }

    /// <summary>
    /// Ensures that if a finish condition is met, the timer stops
    /// </summary>
    /// <param name="notUsed"></param>
    private void StopTimer(GameObject notUsed)
    {
        _gameIsActive = false;
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
        if (_gameIsActive)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
                _timeRemainingText.text = Mathf.Ceil(_timeRemaining).ToString();

                _timerProgressActual -= Time.deltaTime;

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
                _gameIsActive = false;
            }

            // Change the progress of the timer radial
            _timerProgress.fillAmount = _timerProgressActual;
        }
    }
}
