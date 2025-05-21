using Godot;
using System;

public partial class PrologueCar : EnemyAttack
{
    [Export] public Vector2 Direction { get; set; }

    public PrologueCar() : base(100000, 20)
    {
        Shape = new RectangleShape2D()
        {
            Size = new Vector2(50, 100)
        };
    }

    public override void _Ready()
    {
        if (Direction.X == 1 || Direction.X == -1)
            Rotate(Mathf.DegToRad(90));
    }

    public override void _Process(double delta)
    {
        Velocity = (Direction + GlobalPosition.DirectionTo(Global.SceneObjects.Player.GlobalPosition)) * 70000 * (float)delta;
        MoveAndSlide();
    }

    public override void Destroy()
    {
        Shape.Dispose();
    }
}
