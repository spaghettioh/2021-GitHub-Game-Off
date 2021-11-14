using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[CreateAssetMenu (menuName = "Scriptable Objects/Audio Event Channel",
    fileName = "AudioEventChannel")]
public class AudioEventChannelSO : ScriptableObject
{
    public UnityAction<AudioCueSO> OnAudioCued;

    public void PlayAudio(AudioCueSO audioCue)
    {
        OnAudioCued.Invoke(audioCue);
    }
}
