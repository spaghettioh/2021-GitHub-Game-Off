using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UFOlogistSpeechBubble : MonoBehaviour
{
    [SerializeField] private Transform RightArm;
    private float _rightArmPrevious;
    [SerializeField] private Transform RightForearm;
    private float _rightForearmPrevious;
    [SerializeField] private Transform RightHand;
    private float _rightHandPrevious;
    [SerializeField] private Transform LeftArm;
    private float _leftArmPrevious;
    [SerializeField] private Transform LeftForearm;
    private float leftForearmPrevious;
    [SerializeField] private Transform LeftHand;
    private float _leftHandPrevious;
    private float _totalRotation;

    [SerializeField] private float _winningRotationAmount;
    [SerializeField] private FinishEventChannelSO _finishEventChannel;

    [SerializeField] private Image _mask;
    private float _progress = 0f;

    private void Start()
    {
        _mask.fillAmount = _progress;
    }

    private void Update()
    {
        float rightArmCurrent;
        float rightArmDelta;
        float rightForearmCurrent;
        float rightForearmDelta;
        float rightHandCurrent;
        float rightHandDelta;
        float leftArmCurrent;
        float leftArmDelta;
        float leftForearmCurrent;
        float leftForearmDelta;
        float leftHandCurrent;
        float leftHandDelta;

        rightArmCurrent = RightArm.rotation.z * Mathf.Rad2Deg;
        rightArmDelta = Mathf.Abs(rightArmCurrent - _rightArmPrevious);
        _rightArmPrevious = rightArmCurrent;

        rightForearmCurrent = RightForearm.rotation.z * Mathf.Rad2Deg;
        rightForearmDelta = Mathf.Abs(rightForearmCurrent - _rightForearmPrevious);
        _rightForearmPrevious = rightForearmCurrent;

        rightHandCurrent = RightHand.rotation.z * Mathf.Rad2Deg;
        rightHandDelta = Mathf.Abs(rightHandCurrent - _rightHandPrevious);
        _rightHandPrevious = rightHandCurrent;

        leftArmCurrent = LeftArm.rotation.z * Mathf.Rad2Deg;
        leftArmDelta = Mathf.Abs(leftArmCurrent - _leftArmPrevious);
        _leftArmPrevious = leftArmCurrent;

        leftForearmCurrent = LeftForearm.rotation.z * Mathf.Rad2Deg;
        leftForearmDelta = Mathf.Abs(leftForearmCurrent - leftForearmPrevious);
        leftForearmPrevious = leftForearmCurrent;

        leftHandCurrent = LeftHand.rotation.z * Mathf.Rad2Deg;
        leftHandDelta = Mathf.Abs(leftHandCurrent - _leftHandPrevious);
        _leftHandPrevious = leftHandCurrent;

        _totalRotation += (
                rightArmDelta +
                rightForearmDelta +
                rightHandDelta +
                leftArmDelta +
                leftForearmDelta +
                leftHandDelta
            ) / _winningRotationAmount;

        if (!MiniGameFinish.MiniGameIsFinished && _progress < 1f)
        {
            _progress = _totalRotation;

            if (_progress > 1f)
            {
                _progress = 1f;
            }

            _mask.fillAmount = _progress;

            if (_progress == 1f)
            {
                _finishEventChannel.Raise(gameObject);
            }
        }
    }
}
