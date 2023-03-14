using Godot;
using System;

public partial class FactoryElement : Control
{
	private Label factoryNumberLabel;
	private Label factoryResourceLabel;
	private int number;
	public int Number {get; set;}
	public string Resource {get; set;}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Panel/factoryNumberLabel
		factoryNumberLabel = GetNode<Label>("factoryNumberLabel");
		factoryResourceLabel = GetNode<Label>("resourceNameLabel");
	}

	public void SetFactoryNumber(int number){
		Number = number;
		if(factoryNumberLabel == null){
			factoryNumberLabel = GetNode<Label>("factoryNumberLabel");
		}
		factoryNumberLabel.Text = "Factory #: " + Number;
	}

	public void SetFactoryResource(string resource){
		Resource = resource;
		if(factoryResourceLabel == null){
			factoryResourceLabel = GetNode<Label>("resourceNameLabel");
		}
		factoryResourceLabel.Text = "Resource: : " + resource;
	}

	private void _on_viewbtn_pressed(){

	}

	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
