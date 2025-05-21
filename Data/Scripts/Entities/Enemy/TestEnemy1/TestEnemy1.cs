using Godot;
using System;

public partial class TestEnemy1 : EnemyReload
{
    private Player _player;

    public TestEnemy1(float reloadTime, float speed, int damage, int health) : base(reloadTime, speed, damage, health, "TestEnemy1/TestEnemy1.tscn") =>
        Global.SceneObjects.PlayerChanged += TakePlayer;

    public void TakePlayer(Node player) =>
        _player = (Player)player;

    public override void _Process(double delta) =>
        Move(_player?.GlobalPosition ?? new(0, 0), delta);

    public override void TakeDamage(float damage)
    {
        GD.Print($"Enemy take {damage} damage.");
        base.TakeDamage(damage);
    }

    public override void Move(Vector2 playerPosition, double delta)
    {
        if (GlobalPosition.DistanceTo(_player.Shard.GlobalPosition) < 80)
        {
            Attack(new TestEnemyAttack1(Damage, 0.3, GlobalPosition, _player.Shard.GlobalPosition));
            Velocity = -GlobalPosition.DirectionTo(_player.Shard.GlobalPosition) * (float)delta * Speed * 100;
            MoveAndSlide();
        }
        else if (GlobalPosition.DistanceTo(playerPosition) > 60)
        {
            Velocity = GlobalPosition.DirectionTo(playerPosition) * (float)delta * Speed * 100;
            MoveAndSlide();
        }
        else 
        {
            Attack(new TestEnemyAttack1(Damage, 0.3, GlobalPosition, _player.GlobalPosition));
            Velocity = -GlobalPosition.DirectionTo(playerPosition) * (float)delta * Speed * 100 / 2;
            MoveAndSlide();
        }
    }
}
