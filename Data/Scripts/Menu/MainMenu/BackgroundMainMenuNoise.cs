using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class BackgroundMainMenuNoise : TextureRect
{
    private const int TIME = 50;
    private FastNoiseLite noise;

    public override void _Ready()
    {
        if (Texture is NoiseTexture2D noiseTexture)
        {
            noiseTexture.Height = GetWindow().Size.Y / 8;
            noiseTexture.Width = GetWindow().Size.X / 8;
            Tween tween = CreateTween();
            tween.TweenProperty(noiseTexture.Noise, "offset:y", 500, TIME);
            tween.Parallel();
            tween.TweenProperty(noiseTexture.Noise, "offset:z", 500, TIME);
            tween.Chain();
            tween.TweenProperty(noiseTexture.Noise, "offset:y", 0, TIME);
            tween.Parallel();
            tween.TweenProperty(noiseTexture.Noise, "offset:z", 1000, TIME);
            tween.Chain();
            tween.TweenProperty(noiseTexture.Noise, "offset:y", -500, TIME);
            tween.Parallel();
            tween.TweenProperty(noiseTexture.Noise, "offset:z", 500, TIME);
            tween.Chain();
            tween.TweenProperty(noiseTexture.Noise, "offset:y", 0, TIME);
            tween.Parallel();
            tween.TweenProperty(noiseTexture.Noise, "offset:z", 0, TIME);
            tween.SetLoops(int.MaxValue);
        }
    }
}
