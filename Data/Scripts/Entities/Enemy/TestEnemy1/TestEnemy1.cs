using Godot;
using System;

public partial class TestEnemy1 : EnemyReload
{
    public TestEnemy1() : base() { }

    public TestEnemy1(float reloadTime, float speed, int damage, int health) : base(reloadTime, speed, damage, health) { }

    public override void TakeDamage(int damage)
    {
        GD.Print($"Enemy take {damage} damage.");
        base.TakeDamage(damage);
    }

    public override void Attack()
    {
        GetTree().CurrentScene.AddChild(new TestEnemyAttack1(Damage, 1.5));
        Reload();
    }

    public override void Move(Vector2 playerPosition) { }
}
