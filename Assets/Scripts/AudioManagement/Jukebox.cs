using UnityEngine;

public class Jukebox : MonoBehaviour
{
    [SerializeField] private AudioCueSO _music;

    [Header("Broadcasting to...")]
    [SerializeField] private AudioEventChannelSO _audioEventChannel;

    /// <summary>
    /// The Jukebox uses Start() to ensure the audio system set up is done
    /// before it tries to play music
    /// </summary>
    private void Start()
    {
        TriggerMusic();
    }

    private void TriggerMusic()
    {
        _audioEventChannel.RaisePlaybackEvent(_music);
    }
}
