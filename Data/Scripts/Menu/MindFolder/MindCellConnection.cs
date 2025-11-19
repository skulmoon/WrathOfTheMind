using Godot;
using System;
using System.Collections.Generic;

public partial class MindCellConnection : Area2D
{
    private TextureRect _texture;

    public List<MindCellConnection> RepulsionConnections { get; set; } = new List<MindCellConnection>();
    public List<MindCellConnection> RestingConnections { get; set; } = new List<MindCellConnection>();

    public event Action<MindCellConnection> RepulsionConnectionsEntered;
    public event Action<MindCellConnection> RepulsionConnectionsExited;
    public event Action<MindCellConnection> RestingConnectionsEntered;
    public event Action<MindCellConnection> RestingConnectionsExited;

    public int MyProperty { get; set; }

    public override void _Ready()
    {
        _texture = GetNode<TextureRect>("TextureRect");
        Area2D area = GetNode<Area2D>("Repulsion");
        area.AreaEntered += OnRepulsionAreaEntered;
        area.AreaExited += OnRepulsionAreaExited;
        area = GetNode<Area2D>("Resting");
        area.AreaEntered += OnRestingAreaEntered;
        area.AreaExited += OnRestingAreaExited;
    }

    public void OnRepulsionAreaEntered(Area2D area)
    {
        if (area is MindCellConnection connection && area != this)
        {
            RepulsionConnectionsEntered?.Invoke(connection);
            RepulsionConnections.Add(connection);
        }
    }

    public void OnRepulsionAreaExited(Area2D area)
    {
        if (area is MindCellConnection connection && area != this)
        {
            RepulsionConnectionsExited?.Invoke(connection);
            RepulsionConnections.Remove(connection);
        }
    }

    public void OnRestingAreaEntered(Area2D area)
    {
        if (area is MindCellConnection connection && area != this)
        {
            RestingConnectionsEntered?.Invoke(connection);
            RestingConnections.Add(connection);
        }
    }

    public void OnRestingAreaExited(Area2D area)
    {
        if (area is MindCellConnection connection && area != this)
        {
            RestingConnectionsExited?.Invoke(connection);
            RestingConnections.Remove(connection);
        }
    }

    public void SetLineSize(float size) =>
        _texture.Size = new Vector2(2, size + 2);
}
