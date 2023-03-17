using Godot;
using System;

public partial class UpgradePopupPanel : PopupPanel
{

	private UpgradeDialog dialog;
	private int factoryNumber = -1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.PopupCenteredClamped(new Vector2I(462, 308), 0.8f);
		this.Visible = false;
	}

	public void OnClosedHandler(){
		this.Visible = false;
	}
	public void SetFactoryNumber(int num){
		GD.Print("num: " + num);

		factoryNumber = num;
		GD.Print("factoryNumber: " + factoryNumber);
	}

	public void OnFactorySetHandler(int num){
		GD.Print("this should be false: " + dialog == null);

		factoryNumber = num;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
