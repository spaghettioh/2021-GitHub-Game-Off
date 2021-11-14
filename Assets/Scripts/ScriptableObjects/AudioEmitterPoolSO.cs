using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Scriptable Objects/Audio Emitter Pool",
    fileName = "AudioEmitterPool")]
public class AudioEmitterPoolSO : ScriptableObject
{
    [SerializeField] private AudioEmitter _audioEmitterPrefab;
    private Stack<AudioEmitter> _emitterStack = new Stack<AudioEmitter>();
    private Transform _parent;

    public void PreWarm(int count, Transform parent)
    {
        _parent = parent;
        // Create a pool of disabled audio emitters
        for (var i = 0; i < count; i++)
        {
            CreateEmitter();
        }
    }

    private AudioEmitter CreateEmitter()
    {
        AudioEmitter emitter = Instantiate(_audioEmitterPrefab);
        emitter.transform.SetParent(_parent);
        emitter.gameObject.SetActive(false);
        _emitterStack.Push(emitter);
        return emitter;
    }

    public AudioEmitter GetEmitter()
    {
        if (_emitterStack.Count == 0)
        {
            CreateEmitter();
        }

        return _emitterStack.Pop();
    }

    public void ReturnEmitter(AudioEmitter emitter)
    {
        _emitterStack.Push(emitter);
    }

    private void OnDisable()
    {
        _emitterStack.Clear();
    }
}
