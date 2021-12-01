using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTitle : MonoBehaviour
{
    //[SerializeField] private VoidEventChannelSO _waxOffStart;
    [SerializeField] private VoidEventChannelSO _enableControls;

    public void FireAnimationEvent()
    {
        _enableControls.Raise();
    }
}
