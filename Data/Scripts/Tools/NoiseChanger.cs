using Godot;
using System;

public partial class NoiseChanger : Node
{
	private const int TIME = int.MaxValue;
	[Export(PropertyHint.Range, "0,256")] public float multiplier { get; set; }
    [Export] public NoiseTexture2D NoiseTexture { get; set; }

    public override void _Ready()
    {
        if (NoiseTexture != null)
		{
			Tween tween = CreateTween();
			tween.TweenProperty(NoiseTexture.Noise, "offset:z", (long)TIME * multiplier, TIME);
		}
		base._Ready();
	}
}
