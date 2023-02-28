using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private List<Sound> _musics = new List<Sound>();
    [SerializeField] private List<Sound> _sounds = new List<Sound>();

    public bool IsMute { get; private set; } = false;
    public bool IsSoundsMute { get; private set; } = false;
    public AudioSource CurrentMusicPLayingSource { get; private set; }

    public void SetMusicVolume(float volume)
    {
        CurrentMusicPLayingSource.volume = volume;
    }

    public void MuteSounds()
    {
        foreach (var sound in _sounds)
        {
            sound.AudioSource.mute = true;
        }

        IsSoundsMute = true;
    }

    public void UnMuteSounds()
    {
        foreach (var sound in _sounds)
        {
            sound.AudioSource.mute = false;
        }

        IsSoundsMute = false;
    }

    public void MuteMusic()
    {
        foreach (var music in _musics)
        {
            music.AudioSource.mute = true;
        }
    }

    public void UnMuteMusic()
    {
        foreach (var music in _musics)
        {
            music.AudioSource.mute = false;
        }
    }

    public void Mute()
    {
        MuteMusic();
        MuteSounds();

        IsMute = true;
    }

    public void UnMute()
    {
        UnMuteMusic();
        UnMuteSounds();

        IsMute = false;
    }

    public void PlaySound(string soundName)
    {
        var sound = _sounds.FirstOrDefault(sound => sound.Name == soundName);

        if (sound == null)
            throw new Exception($"Sound Error!: \"{soundName}\" not found in sounds!");

        sound.AudioSource.Play();
    }

    public void PlayMusic(string musicName)
    {
        bool isFound = false;

        foreach (var music in _musics)
        {
            if (music.Name == musicName)
            {
                music.AudioSource.Play();
                isFound = true;
                CurrentMusicPLayingSource = music.AudioSource;
                break;
            }
            else
            {
                music.AudioSource.Stop();
            }
        }

        if (!isFound)
            throw new Exception($"Music Error!: \"{musicName}\" not found in musics!");
    }

    public void StopAllMusics()
    {
        foreach (var music in _musics)
        {
            music.AudioSource.Stop();
        }
    }

    public void StopAllSounds()
    {
        foreach (var sound in _sounds)
        {
            sound.AudioSource.Stop();
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);

        InitSounds();

        PlayMusic("Main");
    }

    private void InitSounds()
    {
        foreach (var sound in _sounds)
        {
            sound.AudioSource = gameObject.AddComponent<AudioSource>();

            sound.AudioSource.clip = sound.AudioClip;
            sound.AudioSource.volume = sound.Volume;
            sound.AudioSource.pitch = sound.Pitch;
            sound.AudioSource.playOnAwake = false;
        }

        foreach (var music in _musics)
        {
            music.AudioSource = gameObject.AddComponent<AudioSource>();

            music.AudioSource.clip = music.AudioClip;
            music.AudioSource.volume = music.Volume;
            music.AudioSource.pitch = music.Pitch;
            music.AudioSource.loop = true;
        }

        Debug.Log("Musics and sounds loading success!");
    }
}