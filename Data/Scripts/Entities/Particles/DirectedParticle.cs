using Godot;

public partial class DirectedParticle : DefaultParticle
{
    public Vector2 Direction { get => new Vector2(((ParticleProcessMaterial)ProcessMaterial).Direction.X, ((ParticleProcessMaterial)ProcessMaterial).Direction.Y); set => ((ParticleProcessMaterial)ProcessMaterial).Direction = new Vector3(value.X, value.Y, 0); }

    public override void _Ready()
    {
        Emitting = true;
        Finished += OnFifnfshed;
    }

    public void OnFifnfshed()
    {
        Free();
    }
}
