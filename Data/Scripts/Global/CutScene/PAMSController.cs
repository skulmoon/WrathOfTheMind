using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

public partial class PAMSController : Node
{
    private CutSceneManager _cutSceneManager;
    private LinkedListNode<PAMS> _pamses;
    private List<NPC> _npcs = new List<NPC>();
    private int? _finalCustomize;
    private List<FinalValues> _finalValues;
    private int _endCount = 0;
    private int _npcCount = 0;

    public bool IsDone { get; private set; } = true;

    public PAMSController(CutSceneManager cutSceneManager)
    {
        _cutSceneManager = cutSceneManager;
    }

    public void SetPAMS(Array<PAMS> pamses)
    {
        if (pamses != null)
            _pamses = new LinkedList<PAMS>(pamses).First;
        else 
            _pamses = null;
    }

    public void NextPAMS()
    {
        _npcCount = 0;
        if (_pamses?.Value != null)
        {
            IsDone = false;
            _npcCount = _pamses?.Value?.PAData?.Count ?? 0;
            if (_pamses.Value?.Music != null)
                Global.Music.PlayMusic(_pamses.Value?.Music);
            if (_pamses.Value?.PAData != null)
            {
                _finalCustomize = _pamses.Value?.FinalCustomize;
                _finalValues = _pamses.Value?.FinalValues;
                foreach (PAData paData in _pamses.Value?.PAData)
                    foreach (NPC npc in Global.SceneObjects.Npcs)
                        if (paData.NPCID == npc.ID)
                            npc.GetPA(paData, PADataEnd);
            }
        }
        _pamses = _pamses?.Next;
    }

    private void PADataEnd()
    {
        _endCount++;
        if (_endCount >= _npcCount)
        {
            IsDone = true;
            _endCount = 0;
            if (!_cutSceneManager.IsPanelActive)
            {
                _cutSceneManager.NextCutScenePart();
            }
        }
    }

    public void EndPAMS()
    {
        IsDone = true;
        if (_finalCustomize != null)
            Global.SceneObjects.Location.GetCutSceneCustomize(_finalCustomize ?? 0)?.Invoke();
        if (_finalValues != null)
        {
            foreach (var finalValues in _finalValues)
                foreach (NPC npc in Global.SceneObjects.Npcs)
                    if (finalValues.ID == npc.ID)
                        npc.StopPAData(finalValues);
        }
    }
}
