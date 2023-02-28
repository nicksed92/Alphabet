using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string Name;
    public AudioClip AudioClip;

    [Range(0f, 1f)]
    public float Volume;

    [Range(0.1f, 3f)]
    public float Pitch;

    [HideInInspector]
    public AudioSource AudioSource;
}