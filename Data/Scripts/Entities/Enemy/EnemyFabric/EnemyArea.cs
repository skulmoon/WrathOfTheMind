using Godot;
using System;

public partial class EnemyArea : Area2D
{
    private int _numberOfEnemy = 0;
    [Export] public int Difficulty { get; set; }

    public void PlaceEnemy(Enemy enemy)
    {
        AddChild(enemy);
        Vector2 collisionSize = ((RectangleShape2D)GetNode<CollisionShape2D>("CollisionShape2D").Shape).Size;
        enemy.Visible = true;
        _numberOfEnemy++;
        enemy.Label.Text = _numberOfEnemy.ToString();
        while (true)
        {
            enemy.Position = new Vector2(
                (float)GD.RandRange(-collisionSize.X / 2, collisionSize.X / 2),
                (float)GD.RandRange(-collisionSize.Y / 2, collisionSize.Y / 2)
            );
            GD.Print(enemy.Position);
            var collide = enemy.TestMove(enemy.GetTransform(), new Vector2(0, 0.001f), null, 0, true);
            GD.Print(collide);
            if (collide)
                break;
            }
    }
}
