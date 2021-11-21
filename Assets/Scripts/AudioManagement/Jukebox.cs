using UnityEngine;

public class Jukebox : MonoBehaviour
{
    [SerializeField] private AudioCueSO _music;
    [Header("Listening for...")]
    [SerializeField] private VoidEventChannelSO _waxOff;

    [Header("Broadcasting to...")]
    [SerializeField] private AudioEventChannelSO _audioEventChannel;

    private void OnEnable()
    {
        _waxOff.OnEventRaised += TriggerMusic;
    }

    private void OnDisable()
    {
        _waxOff.OnEventRaised -= TriggerMusic;
    }

    private void TriggerMusic()
    {
        Debug.Log($"Jukebox trigger music {_music.name}");
        _audioEventChannel.Raise(_music);
    }
}
