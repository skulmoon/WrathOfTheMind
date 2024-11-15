using Godot;
using System;
using System.Collections.Generic;

public class NPCDialogue
{
    public int NPCID { get; set; }
    public int DialogueNumber { get; set; }
    public List<IDAndText> Speech { get; set; }
    public List<Options> Options { get; set; }
}