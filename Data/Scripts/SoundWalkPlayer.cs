using Godot;
using System;
using System.Collections.Generic;

public partial class SoundWalkPlayer : Node2D
{
    private Timer _walkTimer;
    private List<AudioStreamPlayer2D> _audioStreamPlayers = new List<AudioStreamPlayer2D>();


    [Export] public bool IsFromGlobalAudio { get; set; }
    [Export] public int AudioPlayersCount { get; set; }
    [Export] public string Sound { get; set; }
    [Export] public float Diffusion { get; set; }
    [Export] public float Interval { get; set; }

    public override void _Ready()
	{
        _walkTimer = new Timer()
        {
            Autostart = false,
            OneShot = false,
            WaitTime = Interval
        };
        AddChild(_walkTimer);
        if (!IsFromGlobalAudio)
        {
            for (int i = 0; i < AudioPlayersCount; i++)
            {
                _audioStreamPlayers.Add(new AudioStreamPlayer2D());
                AddChild(_audioStreamPlayers[i]);
            }
            _walkTimer.Timeout += () => Global.Music.PlaySound(_audioStreamPlayers, Sound, Diffusion);
        }
        else
            _walkTimer.Timeout += () => Global.Music.PlaySound(Sound, Diffusion);
        if (GetParent() is IWalker walker)
        {
            walker.ChangedDirection += OnChangedDirection;
            walker.SpeedMultiperChanged += OnChangedSpeedMultiper;
        }
    }

    public void OnChangedDirection(Vector2 direction)
    {
        if (direction != Vector2.Zero)
        {
            if (_walkTimer.TimeLeft == 0)
            {
                if (!IsFromGlobalAudio)
                    Global.Music.PlaySound(_audioStreamPlayers, Sound, Diffusion);
                else
                    Global.Music.PlaySound(Sound, Diffusion);
                _walkTimer.Start();
            }
        }
        else
            _walkTimer.Stop();
    }

    public void OnChangedSpeedMultiper(float speedMultiper) =>
        _walkTimer.WaitTime = 0.5f / speedMultiper;
}
