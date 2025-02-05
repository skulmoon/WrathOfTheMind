using Godot;
using System;

public partial class NPC : CharacterBody2D
{
    private Vector2 _endPosition;
	private Area2D _interactionArea;

    [Export] public string NPCInteractionPath { get; set; } = "res://Data/Scripts/Entities/NPC/InteractionDefault.cs";
    [Export] public int ID { get; set; }
    [Export] public float Speed { get; set; } = 100;
    public bool IsMove { get; set; }
    public AnimatedSprite2D AnimatedSprite2D { get; set; }

    public void ChangeAnimation(string name)
    {
        AnimatedSprite2D.Animation = name;
        AnimatedSprite2D.Play();
    }

    public void Move(Vector2 startPosition, Vector2 endPosition)
    {
        Position = startPosition;
        _endPosition = endPosition;
        IsMove = true;
    }

    public override void _Ready()
	{
        Global.SceneObjects.Npcs.Add(this);
        AnimatedSprite2D = GetNode<AnimatedSprite2D>("Sprite2D");
        AnimatedSprite2D.Play();
        try
        {
            _interactionArea = GetNode<Area2D>("NPCInteractionArea");
            _interactionArea.SetScript(GD.Load(NPCInteractionPath));
        }
        catch
        {
            GD.Print("Path is incorrect.");
        }
    }

    public override void _ExitTree() =>
        Global.SceneObjects.Npcs.Remove(this);

    public override void _Process(double delta)
    {
        if (IsMove)
        {
            Vector2 direction = (_endPosition - Position);
            float distance = Speed * (float)delta;
            if (direction.Length() > distance)
                MoveAndCollide(direction.Normalized() * distance);
            else
            {
                Position = _endPosition;
                IsMove = false;
            }
        }
    }
}
