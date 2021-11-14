using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private int _poolSize;
    [SerializeField] private AudioEmitterPoolSO _audioEmitterPool;

    [Header("Listening to...")]
    [SerializeField] private AudioEventChannelSO _audioEventChannel;

    private void OnEnable()
    {
        _audioEventChannel.OnAudioCued += ActivateEmitter;
    }

    private void OnDisable()
    {
        _audioEventChannel.OnAudioCued -= ActivateEmitter;
    }

    private void Awake()
    {
        _audioEmitterPool.PreWarm(_poolSize, transform);
    }

    private void ActivateEmitter(AudioCueSO audioCue)
    {
        // Enable an available emitter and send it the audio to play
        AudioEmitter emitter = _audioEmitterPool.GetEmitter();
        emitter.gameObject.SetActive(true);
        emitter.PlayAudio(audioCue);
        emitter.OnEmitterFinished += ReturnEmitterToPool;
    }

    private void ReturnEmitterToPool(AudioEmitter emitter)
    {
        emitter.gameObject.SetActive(false);
        _audioEmitterPool.ReturnEmitter(emitter);
        emitter.OnEmitterFinished -= ReturnEmitterToPool;
    }
}
