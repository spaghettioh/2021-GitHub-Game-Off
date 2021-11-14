using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEmitter : MonoBehaviour
{
    public UnityAction<AudioEmitter> OnEmitterFinished;
    [SerializeField] private AudioSource _audioSource;

    public void PlayAudio(AudioCueSO audioCue)
    {
        AudioClip clip = audioCue.AudioClips[Random.Range(0, audioCue.AudioClips.Count - 1)];
        _audioSource.PlayOneShot(clip);
        StartCoroutine(WrapUp(clip.length));
    }

    /// <summary>
    /// Waits for the clip to finish playing, then resets the emitter object
    /// </summary>
    /// <param name="length">The length of the audio clip (seconds)</param>
    /// <returns></returns>
    private IEnumerator WrapUp(float length)
    {
        yield return new WaitForSeconds(length);
        _audioSource.clip = null;
        OnEmitterFinished.Invoke(this);
    }
}
