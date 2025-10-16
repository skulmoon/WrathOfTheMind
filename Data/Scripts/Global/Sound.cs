using Godot;
using System;
using System.Collections.Generic;

public class Sound
{
    private List<AudioStreamPlayer> _audioPlayers = new List<AudioStreamPlayer>();
    private AudioStreamPlayer _music = new AudioStreamPlayer();
	private string _soundPath = "res://Data/Sounds/Effects/";
	private string _musicPath = "res://Data/Sounds/Musics/";
	private RandomNumberGenerator _random = new RandomNumberGenerator();

    public Sound()
	{
        _music.Bus = "Music";
        Global.SceneObjects.StorageReady += (storage) =>
        {
            for (int i = 0; i < 15; i++)
            {
                _audioPlayers.Add(new AudioStreamPlayer());
                _audioPlayers[i].Bus = "Sound";
                storage.AddChild(_audioPlayers[i]);
            }
            storage.AddChild(_music);
        };
    }

    public void PlaySound(AudioStreamPlayer streamPlayer, string sound, float diffusion = 0)
    {
        if (streamPlayer != null)
        {
            streamPlayer.Bus = "Sound";
            streamPlayer.PitchScale = 1 + _random.RandfRange(-diffusion, diffusion);
            streamPlayer.Stream = ResourceLoader.Load<AudioStream>(_soundPath + sound);
            streamPlayer.Play();
        }
    }

    public void PlaySound(string sound, float diffusion = 0) =>
        PlaySound(_audioPlayers.Find(x => !x.Playing), sound, diffusion);

    public void PlayMusic(string music)
    {
        if (music != null)
        {
            _music.Stream = ResourceLoader.Load<AudioStream>(_musicPath + music);
            _music.Play();
        }
        else
            _music.Stop();
    }

    public bool CheckMusic(string music) =>
        _music.Stream == ResourceLoader.Load<AudioStream>(_musicPath + music) && _music.Playing;
}
