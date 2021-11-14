using UnityEngine;

public class Jukebox : MonoBehaviour
{
    [SerializeField] private AudioCueSO _music;

    [Header("Broadcasting to...")]
    [SerializeField] private AudioEventChannelSO _audioEventChannel;

    /// <summary>
    /// The Jukebox uses Start() to ensure all the audio things have been set up
    /// before it tries to play music
    /// </summary>
    private void Start()
    {
        TriggerMusic();
    }

    private void TriggerMusic()
    {
        _audioEventChannel.RequestPlayback(_music);
    }
}
