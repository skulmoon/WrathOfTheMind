using Godot;
using System;
using Godot.Collections;

[GlobalClass]
public partial class NPCDialogues : Resource
{
    [Export] public Array<NPCDialogue> nPCDialogues { get; set; }
}
