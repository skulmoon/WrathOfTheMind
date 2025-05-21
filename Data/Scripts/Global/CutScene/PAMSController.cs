using Godot;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

public partial class PAMSController : Node
{
    public bool IsDone { get; private set; } = true;

    private LinkedListNode<PAMS> _pamses;
    private List<NPC> _npcs = new List<NPC>();
    private int _endCount = 0;

    public void SetPAMS(List<PAMS> pamses)
    {
        if (pamses != null)
            _pamses = new LinkedList<PAMS>(pamses).First;
        else 
            _pamses = null;
    }

    public void NextPAMS()
    {
        if (_pamses != null)
        {
            IsDone = false;
            if (_pamses.Value.Music != null)
                Global.Music.PlayMusic(_pamses.Value.Music);
            if (_pamses.Value.PAData != null)
            {
                foreach (PAData paData in _pamses.Value.PAData)
                    foreach (NPC npc in Global.SceneObjects.Npcs)
                        if (paData.NPCID == npc.ID)
                            npc.GetPA(paData, PADataEnd);
            }
        }
    }

    private void PADataEnd()
    {
        _endCount++;
        if (_endCount >= _npcs.Count)
        {
            IsDone = true;
            _endCount = 0;
            _pamses = _pamses?.Next ?? _pamses;
        }
    }

    public void EndPAMS()
    {
        IsDone = true;
        if (_pamses?.Value != null)
        {
            if (_pamses?.Value?.FinalCustomize != null)
                Global.SceneObjects.Location.GetCutSceneCustomize(_pamses.Value.FinalCustomize ?? 0)?.Invoke();
            foreach (var finalValues in _pamses.Value.FinalValues)
                foreach (NPC npc in Global.SceneObjects.Npcs)
                    if (finalValues.ID == npc.ID)
                        npc.StopPAData(finalValues);
        }
    }
}
