using Godot;
using Newtonsoft.Json.Linq;
using System;
using System.Transactions;

public partial class SoundSlider : BaseSlider
{
    private double _pastValue;
    [Export] public string BusName { get; set; }

    public override void _Ready()
    {
        _pastValue = AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex(BusName));
        base._Ready();
        Value = _pastValue;
    }

    public override void OnValueChanged(float value)
    {
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex(BusName), value);
        base.OnValueChanged(value);
    }

    public void ComeBackValue()
    {
        Value = _pastValue;
        AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex(BusName), (float)Value);
    }
}
