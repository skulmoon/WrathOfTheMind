using Godot;
using System;
using System.Collections.Generic;

public partial class VitalikInteractionArea : NPCInteractionArea
{
    private List<string> speech;
    private DialogPanel dialoguePanel;

    public override void _Ready()
    {
        dialoguePanel = GetNode<DialogPanel>("/root/Test/Interface/DialogPanel");
    }

    public override void InteractionWithNPC() 
    {
        GD.Print("Interaction with Vitalik.");

        speech = Global.DialogueManager.GetDialogue(1, 1);

        dialoguePanel.Show();

        if (speech != null)
        {
            GD.Print(speech[0]);
            GD.Print(speech[1]);
        }
    }
}
