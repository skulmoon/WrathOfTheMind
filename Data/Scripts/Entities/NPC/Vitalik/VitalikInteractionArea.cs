using Godot;
using System;
using System.Collections.Generic;

public partial class VitalikInteractionArea : Area2D, IInteractionArea
{
    public void Interaction() 
    {
        GD.Print("Interaction with Vitalik.");
        int choice = Global.Dialogue.GetChoice(1, 1);
        if (choice == -1)
            Global.Dialogue.GetDialogue(1, 1);
        else
            Global.Dialogue.GetDialogue(1, choice);
    }
}
