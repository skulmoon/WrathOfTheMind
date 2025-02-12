using Godot;
using System;

public partial class BaseSlider : HSlider
{
	private TextureRect _nodeGrabber;
    [Export] public Texture2D Grabber { get; set; }
	[Export] public Texture2D GrabberHighlight { get; set; }

	public override void _Ready()
	{
		_nodeGrabber = GetNode<TextureRect>("Grabber");
		_nodeGrabber.Texture = Grabber;
		_nodeGrabber.OffsetLeft = -_nodeGrabber.Size.Y / 2;
		_nodeGrabber.OffsetRight = _nodeGrabber.Size.Y / 2;
		OnValueChanged((float)Value);
    }

	public void OnMouseEntered()
	{
		_nodeGrabber.Texture = GrabberHighlight;
    }

	public void OnMouseExited()
	{
		_nodeGrabber.Texture = Grabber;
    }

	public void OnValueChanged(float value)
	{
		GD.Print(value);
		float newPosition = (float)((value - MinValue) / (MaxValue - MinValue));
		_nodeGrabber.AnchorRight = newPosition;
		_nodeGrabber.AnchorLeft = newPosition;
    }
}
