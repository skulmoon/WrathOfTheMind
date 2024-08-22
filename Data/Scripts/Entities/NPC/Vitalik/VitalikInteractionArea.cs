using Godot;
using System;
using System.Collections.Generic;

public partial class VitalikInteractionArea : Area2D, IInteractionArea
{
    public void Interaction() 
    {
        GD.Print("Interaction with Vitalik.");
        Global.DialogueManager.GetDialogue(1, 1);
    }
}
