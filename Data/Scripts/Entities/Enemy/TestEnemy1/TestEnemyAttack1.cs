using Godot;
using System;

public partial class TestEnemyAttack1 : EnemyAttack
{
    public TestEnemyAttack1(int damage, double lifeTime) : base(damage, lifeTime)
    {
        AddChild(new CollisionShape2D()
        {
            Shape = new CircleShape2D()
            {
                Radius = 10
            }
        });
    }
}
