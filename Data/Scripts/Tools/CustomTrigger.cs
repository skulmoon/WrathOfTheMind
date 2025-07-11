using Godot;
using System;

public partial class CustomTrigger : Area2D
{
    public CollisionShape2D CollisionShape { get; set; }
    public Shape2D Shape { get => CollisionShape.Shape; }

    public CustomTrigger(uint collisionMask, Shape2D shape, BodyEnteredEventHandler bodyEntered = null, BodyExitedEventHandler bodyExited = null)
    {
        CollisionMask = collisionMask;
        if (bodyEntered != null)
            BodyEntered += bodyEntered;
        if (bodyExited != null)
            BodyExited += bodyExited;
        CollisionShape = new CollisionShape2D
        {
            Shape = shape
        };
        AddChild(CollisionShape);
    }
}
