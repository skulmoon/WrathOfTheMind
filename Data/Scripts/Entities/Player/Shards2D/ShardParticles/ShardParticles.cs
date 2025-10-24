using Godot;
using System;

public partial class ShardParticles : Node2D
{
    private GradientTexture1D Gradient;
    [Export] public GpuParticles2D Particles { get; set; }

    public void ChangeColor(Color color)
    {
        Particles.Modulate = color;
        Gradient = new GradientTexture1D();
        Gradient.Gradient = new Gradient();
        GD.Print(Gradient.Gradient.GetColor(0));
        GD.Print(Gradient.Gradient.GetColor(1));
        GD.Print(Gradient.Gradient.Colors.Length);
        Gradient.Gradient.SetColor(0, color);
        Gradient.Gradient.SetColor(1, new Color(1, 1, 1, 0));
        GD.Print(Gradient.Gradient.GetColor(0));
        GD.Print(Gradient.Gradient.GetColor(1));
        GD.Print(Gradient.Gradient.Colors.Length);
        ((ParticleProcessMaterial)Particles.ProcessMaterial).ColorRamp = Gradient;
    }
}
