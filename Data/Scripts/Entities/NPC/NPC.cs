using Godot;
using System;
using System.Collections.Generic;

public partial class NPC : CharacterBody2D
{
    private LinkedListNode<(Vector2 Position, string Animation, float? Time, int? Customize)> _nextPAData;
	private Area2D _interactionArea;
    private Timer _timer = new Timer { OneShot = true };

    [Export] public string NPCInteractionPath { get; set; } = "res://Data/Scripts/Entities/NPC/InteractionDefault.cs";
    [Export] public int ID { get; set; }
    [Export] public float Speed { get; set; } = 60;
    public bool IsMove { get; set; }
    public AnimatedSprite2D AnimatedSprite2D { get; set; }

    public override void _Ready()
	{
        AddChild(_timer);
        _timer.Timeout += ActivePA;
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
            Vector2 direction = (_nextPAData.Value.Position - Position);
            if (direction.Length() > Speed * (float)delta)
            {
                Velocity = Velocity.Lerp(direction.Normalized() * Speed, 20 * (float)delta);
                MoveAndSlide();
            }
            else if (_timer.TimeLeft == 0)
                ActivePA();
        }
    }

    public void GetPA(PAData PAData)
    {
        var list = new LinkedList<(Vector2 Position, string Animation, float? Time, int? Customize)>(PAData.Data);
        _nextPAData = list.First;
        ActivePA();
        IsMove = true;
        ProcessMode = ProcessModeEnum.Always;
    }

    public void ActivePA()
    {
        if (_nextPAData?.Value.Customize != null)
            ((Location)GetTree().CurrentScene).GetCutSceneCustomize((int)_nextPAData.Value.Customize)?.Invoke();
        if (_nextPAData?.Next != null)
        {
            Position = _nextPAData.Value.Position;
            _nextPAData = _nextPAData.Next;
            if (_nextPAData.Value.Animation != null)
            {
                AnimatedSprite2D.Animation = _nextPAData.Value.Animation;
                AnimatedSprite2D.Play();
            }
            if (_nextPAData.Value.Time != null)
                _timer.Start(_nextPAData.Value.Time ?? 0);
        }
        else
        {
            Position = _nextPAData.Value.Position;
            IsMove = false;
            ProcessMode = ProcessModeEnum.Disabled;
        }
    }
}
