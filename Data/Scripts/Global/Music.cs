using Godot;
using System;

public class Music
{
	private AudioStreamPlayer _music = new AudioStreamPlayer();
	private string _soundPath = "res://Data/Sounds/Effects/";
	private string _musicPath = "res://Data/Sounds/Musics/";
	private Random _random = new Random();

    public Music() =>
		_music.Bus = "Music";

	public void PlaySound(AudioStreamPlayer streamPlayer, string sound)
	{
		streamPlayer.Bus = "Sound";
		streamPlayer.Stream = ResourceLoader.Load<AudioStream>(_soundPath + sound);
		streamPlayer.Play();
	}

    public void PlaySound(AudioStreamPlayer streamPlayer, string sound, float Diffusion)
    {
		streamPlayer.PitchScale = 1 + _random.NextSingle() * (Diffusion * 2) - Diffusion;
		PlaySound(streamPlayer, sound);
    }

    public void PlayMusic(string music)
    {
        _music.Stream = ResourceLoader.Load<AudioStream>(_musicPath + music);
        _music.Play();
    }
}
