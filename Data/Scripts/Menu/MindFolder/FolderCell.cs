using Godot;
using System;

public partial class FolderCell : MindCell
{
    private bool _isOpen;

    [Export] public bool IsActive { get; set; }
    [Export] public MindCell[] MindCells { get; set; }

    public bool IsOpen 
    { 
        get => _isOpen; 
        set => _isOpen = value; 
    }

    private Texture2D _normalTexture;

    public FolderCell() : base("res://Data/Textures/MindFolder/FolderCell.png", "res://Data/Textures/MindFolder/PressedFolderCell.png")
    {
        _normalTexture = Button.TextureNormal;
        Button.Pressed += OnPressed;
    }

    public void OnPressed()
    {
        if (!_isOpen)
        {
            foreach (var cell in MindCells)
                cell.Activate();
        }
        else
        {
            foreach (var cell in MindCells)
                cell.Deactivate();
        }
        _isOpen = !_isOpen;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (_isOpen)
        {
            foreach (var cell in MindCells)
            {
                cell.Line.SetLineSize(cell.GlobalPosition.DistanceTo(GlobalPosition));
                cell.Line.Rotation = cell.GlobalPosition.AngleToPoint(GlobalPosition) + Mathf.DegToRad(-90);
            }
        }
    }
}
