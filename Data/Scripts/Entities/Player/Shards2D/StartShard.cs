using Godot;
using System;

public partial class StartShard : Shard2D
{
    public StartShard(Action<Shard2D> zeroHealth, Shard shard, int number) : base(zeroHealth, shard, number)
    {
        Sprite.Texture = GD.Load<Texture2D>("res://Data/Textures/Entities/Shards/StartShard.png");
        Light.Color = new Color(0.2f, 1, 1);
        EndParticles.Add(GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/Particles/StartShard/StartShardParticlesDestroyed.tscn").Instantiate<DirectedParticle>());
    }
}
