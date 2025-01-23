using Godot;
using System;

public partial class EnemyArea : Area2D
{
    [Export] public int Difficulty { get; set; }

    public void PlaceEnemy(Enemy enemy)
    {
        AddChild(enemy);
        Vector2 collisionSize = ((RectangleShape2D)GetNode<CollisionShape2D>("CollisionShape2D").Shape).Size;
        enemy.Position = new Vector2(
            (float)GD.RandRange(-collisionSize.X / 2, collisionSize.X / 2),
            (float)GD.RandRange(-collisionSize.Y / 2, collisionSize.Y / 2)
        );
    }
}
