using Godot;
using System;

public partial class UpgradeDialog : Control
{

	private Label factoryNumberLabel;
	private Label productLabel;
	private int factoryNumber = -1;
	private bool isPositionCentered = false;
	[Signal]
	public delegate void OnClosedPanelEventHandler();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		factoryNumberLabel = GetNode<Label>("Panel/factoryNumberLabel");
		
		productLabel = GetNode<Label>("Panel/productLabel");

		if(!isPositionCentered){
			var globalRec = GetGlobalRect().GetCenter();
			this.GlobalPosition = globalRec;
			isPositionCentered = true;
		}
	}

	public void SetFactoryNumber(int num){

		factoryNumber = num;

		if(factoryNumberLabel == null){
			factoryNumberLabel = GetNode<Label>("Panel/factoryNumberLabel");
		}

		factoryNumberLabel.Text = "Factory #: " + factoryNumber;
	}

	public void OnFactorySetHandler(int num){
		GD.Print("Num: " + num);
		if(factoryNumberLabel == null){
			factoryNumberLabel = GetNode<Label>("Panel/factoryNumberLabel");
		}
		factoryNumber = num;
		factoryNumberLabel.Text = "Factory #: " + factoryNumber;
		GD.Print("factor number: " + num);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(!isPositionCentered){
			var globalRec = GetGlobalRect().GetCenter();
			this.GlobalPosition = globalRec;
			isPositionCentered = true;
		}
	}

	private void _on_slot_one_btn_pressed(){

	}

	private void _on_slot_two_btn_pressed(){

	}

	private void _on_slot_three_btn_pressed(){
		
	}

	private void _on_closebtn_pressed(){
		EmitSignal("OnClosedPanel");
	}
}
