using Godot;
using System;
using System.Collections.Generic;

public partial class VitalikInteractionArea : Area2D, IInteractionArea
{
    public void Interaction() 
    {
        GD.Print("Interaction with Vitalik.");
        int choice = Global.CutSceneData.GetChoice(1, 1);
        if (choice == -1)
            Global.CutSceneManager.OutputCutScene(1, 1);
        else
            Global.CutSceneManager.OutputCutScene(1, choice);
    }
}
