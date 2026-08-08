using Godot;
using System;
using System.Collections.Generic;

public partial class ShardCell : TextureRect
{
    private const string ABILITY_PATH = "res://Data/Textures/Entities/Shards/";

    private Shard2D _shard;

    [Export] public int ShardNumber { get; set; } = 0;
    [Export] public TextureRect AbilityTexture { get; set; }
    [Export] public TextureRect ShardTexture { get; set; }
    [Export] public ProgressBar HealthBar { get; set; }

    public ShardCell()
    {
        Global.SceneObjects.PlayerChanged += OnPlayerChanged;
    }

    public void OnPlayerChanged(Player player)
    {
        player.Shard.ShardsChanged += OnShardChanged;
        player.Shard.ReloadCompleted += OnShardChanged;
        if ((player.Shard.ActiveShards?.Count ?? -1) > ShardNumber)
            _shard = player.Shard.ActiveShards[ShardNumber];
    }

    public void OnShardChanged(List<Shard2D> shards)
    {
        if ((shards?.Count ?? -1) > ShardNumber)
        {
            if (_shard != null)
                _shard.HealthChanged -= OnHealthChanged;
            if (shards[ShardNumber] is Shard2D shard)
            {
                _shard = shard;
                HealthBar.MaxValue = _shard.MaxHealth;
                _shard.HealthChanged += OnHealthChanged;
                if (shard is ShardAbility shardAbility)
                {
                    AbilityTexture.Texture = GD.Load<Texture2D>($"{ABILITY_PATH}{shardAbility.GetType()}/{shardAbility.GetType()}Abilities.png");
                    ShardTexture.Texture = GD.Load<Texture2D>($"{ABILITY_PATH}{shardAbility.GetType()}.png");
                }
                else
                {
                    ShardTexture.Texture = GD.Load<Texture2D>($"{ABILITY_PATH}{shard.GetType()}.png");
                    AbilityTexture.Texture = GD.Load<Texture2D>($"{ABILITY_PATH}EmptyAbilities.png");
                }
                if (shard.IsMain && shard.IsEnabled)
                    CreateTween().TweenProperty(this, "modulate:a", 1, 0.5f);
                else
                    CreateTween().TweenProperty(this, "modulate:a", 0.5f, 0.5f);
            }
        }
        else
        {
            ShardTexture.Texture = null;
            AbilityTexture.Texture = GD.Load<Texture2D>($"{ABILITY_PATH}EmptyAbilities.png");
            CreateTween().TweenProperty(this, "modulate:a", 0, 0.5f);
        }
    }

    public void OnHealthChanged(int health) =>
        HealthBar.Value = health;
}
