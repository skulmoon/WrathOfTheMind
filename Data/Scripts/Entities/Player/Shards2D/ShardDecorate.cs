using Godot;
using System;
using System.Collections.Generic;
using static Godot.TextServer;

public class ShardDecorate
{
    private float _delta = 0;
    private Player _player;

    public ShardDecorate() =>
        Global.SceneObjects.OnPlayerChanged += ChangePlayer;

    public void ChangePlayer(Node player) => 
        _player = (Player)player;

	public void DecorateMainShard(ShardManager manager, Shard2D shard, Vector2 cursorPosition, float delta)
	{
        _delta += (float)delta * 10;
        shard.Light.Energy = MathF.Sin(_delta) * 0.1f + 0.9f;
        Vector2 direction = manager.Position.DirectionTo(cursorPosition);
        if (manager.GlobalPosition.DistanceTo(cursorPosition) > (float)delta * shard.Speed)
            shard.Rotation = Vector2.FromAngle(shard.Rotation).Lerp(Vector2.FromAngle(shard.GlobalPosition.AngleToPoint(cursorPosition) + (45 * MathF.PI / 180) + (MathF.Sin(_delta) / 10)), 20 * (float)delta).Angle();
        else
            shard.Rotation = Vector2.FromAngle(shard.Rotation).Lerp(Vector2.FromAngle((-45 * MathF.PI / 180) + (MathF.Sin(_delta) / 10)), 10 * (float)delta).Angle();
        if (cursorPosition.DistanceTo(_player?.GlobalPosition ?? Vector2.Zero) > shard.MaxRange)
            cursorPosition = (_player?.GlobalPosition ?? Vector2.Zero).DirectionTo(cursorPosition) * shard.MaxRange + _player?.GlobalPosition ?? Vector2.Zero;
        if (manager.GlobalPosition.DistanceTo(cursorPosition) > (float)delta * shard.Speed)
            manager.GlobalPosition += manager.GlobalPosition.DirectionTo(cursorPosition) * (float)delta * shard.Speed;
        else
            manager.GlobalPosition = cursorPosition;
    }

    public void DecorateSubordinateShards(List<Shard2D> shards)
    {
        float angelDistance = 2 * MathF.PI / shards.Count;
        for (int i = 0; i < shards.Count; i++)
        {
            Vector2 newPosition = new Vector2(Mathf.Cos(angelDistance * i + _delta), Mathf.Sin(angelDistance * i + _delta)) * 20;
            shards[i].Rotation = shards[i].Position.AngleToPoint(newPosition) + (45 * MathF.PI / 180);
            shards[i].Position = newPosition;
        }
    }

    public void StartReload(ShardManager manager, Shard2D shard)
    {
        manager.TopLevel = false;
        manager.Position = Vector2.Zero;
        manager.Visible = false;
        shard.Light.Energy = 0;
        shard.DisableMode = CollisionObject2D.DisableModeEnum.KeepActive;
    }

    public void CompleteReload(ShardManager manager, Shard2D shard, List<Shard2D> shards)
    {
        manager.TopLevel = true;
        manager.GlobalPosition = Global.SceneObjects.Player?.Position ?? Vector2.One;
        manager.Visible = true;
        shard.Light.Energy = 1;
        for (int i = 0; i < shards.Count; i++)
        {
            shards[i].Visible = true;
            shards[i].DisableMode = CollisionObject2D.DisableModeEnum.KeepActive;
        }
        shard.Visible = true;
        shard.DisableMode = CollisionObject2D.DisableModeEnum.KeepActive;
    }
}
