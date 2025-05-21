using Godot;
using Microsoft.VisualBasic;
using System;

public partial class DialogueStarter : Area2D, IInteractionArea
{
    [Export] public int NPCID { get; set; }
    [Export] public int Number { get; set; }

    public void Interaction()
    {
        if (!Global.CutSceneManager.IsPanelActive)
        {
            GD.Print(60);
            Global.CutSceneManager.OutputCutScene(NPCID, Number);
        }
    }
}
