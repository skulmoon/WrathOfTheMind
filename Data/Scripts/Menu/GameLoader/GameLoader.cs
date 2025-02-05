using Godot;
using System;
using System.Collections.Generic;

public partial class GameLoader : Control
{
    private VBoxContainer _container;
    private int _buttonCount = 0;
    private TextureButton _exitButton;
	private TextureButton _loadButton;
	private TextureButton _deleteButton;
	private string _currentSave;

	public override void _Ready()
	{
        _container = GetNode<VBoxContainer>("/root/GameLoader/Saves/VBoxContainer");
        for (int i = 0; i < Global.Settings.Saves.Count; i++)
        {
            var button = new SaveButton();
            button.Text = Global.Settings.Saves[i].Name;
            button.Name = Global.Settings.Saves[i].Name;
            button.CurrentSaveName += ChangeCurrentButton;
            _container.AddChild(button);
        }
        _buttonCount = Global.Settings.Saves.Count;
        _exitButton = GetNode<TextureButton>("Options/Exit");
        _exitButton.Pressed += OnPressedExit;
        _loadButton = GetNode<TextureButton>("Options/Load");
        _loadButton.Pressed += OnPressedLoad;
        _deleteButton = GetNode<TextureButton>("Options/Delete"); 
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
            Global.JSON.LoadGame(_currentSave);
            GetTree().ChangeSceneToFile($"res://Data/Scenes/Location/{Global.Settings.GameSettings.CurrentLocation}.tscn");
        }
    }
    public void OnPressedDelete()
    {
        if (_currentSave != null)
        {
            _container.RemoveChild(GetNode($"/root/GameLoader/Saves/VBoxContainer/{_currentSave}"));
            Global.JSON.DeleteGame(_currentSave);
        }
    }
}
