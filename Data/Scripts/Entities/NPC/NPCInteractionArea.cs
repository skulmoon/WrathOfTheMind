using Godot;
using System;

public partial class NPCInteractionArea : Area2D
{
    public virtual void InteractionWithNPC()
    {
        GD.Print("Stupid idiot, you forgot to change the script!");
    }
}
