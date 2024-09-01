using Godot;
using System;
using System.Collections.Generic;

public partial class VitalikInteractionArea : Area2D, IInteractionArea
{
    public void Interaction() 
    {
        GD.Print("Interaction with Vitalik.");
        int choice = Global.DialogueManager.GetChoice(1, 1);
        if (choice == -1)
            Global.DialogueManager.GetDialogue(1, 1);
        else
            Global.DialogueManager.GetDialogue(1, choice);
    }
}
