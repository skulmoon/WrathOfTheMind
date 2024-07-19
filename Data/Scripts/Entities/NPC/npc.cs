using Godot;
using System;

public partial class npc : CharacterBody2D
{
	private NPCInteractionArea _interactionArea;

    [Export] public string NPCInteractionPath { get; set; } = "res://Data/Scripts/Entities/NPC/NPCInteractionArea.cs";

    public override void _Ready()
	{
        try
        {
            _interactionArea = GetNode<NPCInteractionArea>("NPCInteractionArea");
            _interactionArea.SetScript(GD.Load(NPCInteractionPath));
        }
        catch
        {
            GD.Print("Path is incorrect.");
        }
    }
}