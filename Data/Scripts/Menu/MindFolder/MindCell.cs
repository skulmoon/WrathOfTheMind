using Godot;
using System;

public partial class MindCell : Node2D
{
    public Vector2 CenterPosition = Vector2.Zero;
    public float Speed { get; set; } = 50;
    public TextureButton Button { get; private set; }
    public Node2D ButtonScaler { get; private set; }
    public MindCellConnection Line { get; private set; }

    public MindCell(string textureNormal = "", string texturePressed = "")
    {
        Line = GD.Load<PackedScene>("res://Data/Scenes/Menu/MindFolder/mind_cell_connection.tscn").Instantiate<MindCellConnection>();
        AddChild(Line);
        ButtonScaler = new Node2D();
        AddChild(ButtonScaler);
        Button = new TextureButton();
        ButtonScaler.AddChild(Button);
        Button.TextureNormal = GD.Load<Texture2D>(textureNormal);
        Button.TexturePressed = GD.Load<Texture2D>(texturePressed);
        Button.MouseExited += OnMouseExited;
        Button.MouseEntered += OnMouseEntered;
    }

    public override void _Ready()
    {
        Button.Position = new Vector2(-16, -16);
        Button.Size = new Vector2(32, 32);
        CenterPosition = GetViewport().GetWindow().Size / 2 / 3;
    }

    public void OnMouseExited()
    {
        CreateTween().TweenProperty(ButtonScaler, "scale", new Vector2(1, 1), 0.2f).SetTrans(Tween.TransitionType.Sine);
    }

    public void OnMouseEntered()
    {
        CreateTween().TweenProperty(ButtonScaler, "scale", new Vector2(1.1f, 1.1f), 0.2f).SetTrans(Tween.TransitionType.Sine);
    }

    public override void _Process(double delta)
    {
        Position += GetGravityVector() * (float)delta * Speed;
    }

    public Vector2 GetGravityVector()
    {
        Vector2 vector = Vector2.Zero;
            vector = GlobalPosition.DirectionTo(CenterPosition);
        return vector;
    }

    public void Activate()
    {
        Visible = true;
        ProcessMode = ProcessModeEnum.Inherit;
    }

    public void Deactivate()
    {
        Visible = false;
        ProcessMode = ProcessModeEnum.Disabled;
    }
}
