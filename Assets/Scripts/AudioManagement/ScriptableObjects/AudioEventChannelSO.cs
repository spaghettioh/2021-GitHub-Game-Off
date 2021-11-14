using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu (menuName = "Scriptable Objects/Audio Event Channel",
    fileName = "AudioEventChannel")]
public class AudioEventChannelSO : ScriptableObject
{
    public UnityAction<AudioCueSO> OnPlaybackRequested;

    /// <summary>
    /// Raises a playback event
    /// </summary>
    /// <param name="audioCue"></param>
    public void RequestPlayback(AudioCueSO audioCue)
    {
        OnPlaybackRequested.Invoke(audioCue);
    }
}
