using Godot;
using System;
using System.Collections.Generic;

public partial class GameLoader : Control
{
    private VBoxContainer _container;
    private int _buttonCount = 0;
    private Button _exitButton;
	private Button _loadButton;
	private Button _deleteButton;
	private string _currentSave;

	public override void _Ready()
	{
        List<Save> saves;
        saves = Global.DirectoryManager.GetSaveNames();
        saves.Sort();
        _container = GetNode<VBoxContainer>("/root/GameLoader/Saves/VBoxContainer");
        for (int i = 0; i < saves.Count; i++)
        {
            var button = new SaveButtons();
            button.Text = saves[i].Name;
            button.Name = saves[i].Name;
            button.CurrentSaveName += ChangeCurrentButton;
            _container.AddChild(button);
        }
        _buttonCount = saves.Count;
        _exitButton = GetNode<Button>("Options/Exit");
        _exitButton.Pressed += OnPressedExit;
        _loadButton = GetNode<Button>("Options/Load");
        _loadButton.Pressed += OnPressedLoad;
        _deleteButton = GetNode<Button>("Options/Delete"); 
        _deleteButton.Pressed += OnPressedDelete;
    }

	public void ChangeCurrentButton(string name)
	{
        _currentSave = name;
    }

	public void OnPressedExit()
	{
        GetTree().ChangeSceneToFile("res://Data/Scenes/Menu/MainMenu.tscn");
    }
    public void OnPressedLoad()
    {
        if (_currentSave != null)
        {
            Global.JSONManager.LoadGame(_currentSave);
            Global.DialogueManager.LoadDialogues();
            GetTree().ChangeSceneToFile($"res://Data/Scenes/Location/{Global.Settings.CurrentLocation}.tscn");
        }
    }
    public void OnPressedDelete()
    {
        if (_currentSave != null)
        {
            _container.RemoveChild(GetNode($"/root/GameLoader/Saves/VBoxContainer/{_currentSave}"));
            Global.DirectoryManager.DeleteSave(_currentSave);
        }
    }
}
