using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWipeManager : MonoBehaviour
{
    [SerializeField] private Animator _screenWipeAnimator;

    [Header("Next scene requested, wipe screen")]
    [SerializeField] private VoidEventChannelSO _waxOn;
    [Header("Screen wiped, load the next scene")]
    [SerializeField] private VoidEventChannelSO _waxOnFinished;
    [Header("Next scene loaded, unwipe screen")]
    [SerializeField] private VoidEventChannelSO _waxOff;
    [Header("Wipe removed, begin mini game")]
    [SerializeField] private VoidEventChannelSO _waxOffFinished;

    private void OnEnable()
    {
        _waxOff.OnEventRaised += TriggerWaxOff;
        _waxOn.OnEventRaised += TriggerWaxOn;
    }

    private void OnDisable()
    {
        _waxOff.OnEventRaised -= TriggerWaxOff;
        _waxOn.OnEventRaised -= TriggerWaxOn;
    }

    private void TriggerWaxOn()
    {
        StartCoroutine(WaxOn());
    }

    private void TriggerWaxOff()
    {
        StartCoroutine(WaxOff());
    }

    private IEnumerator WaxOn()
    {
        _screenWipeAnimator.SetTrigger("WaxOn");

        yield return new WaitForSeconds(_screenWipeAnimator
            .GetCurrentAnimatorStateInfo(0).length);

        _waxOnFinished.Raise();
    }

    private IEnumerator WaxOff()
    {
        _screenWipeAnimator.SetTrigger("WaxOff");

        yield return new WaitForSeconds(_screenWipeAnimator
            .GetCurrentAnimatorStateInfo(0).length);

        _waxOffFinished.Raise();
    }
}
