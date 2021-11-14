using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Audio Cue",
    fileName = "SFXorMusic_NAME")]
public class AudioCueSO : ScriptableObject
{
    public List<AudioClip> AudioClips;
}
