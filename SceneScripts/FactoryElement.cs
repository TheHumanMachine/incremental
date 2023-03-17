using Godot;
using System;

public partial class FactoryElement : Control
{
	private Label factoryNumberLabel;
	private Label factoryResourceLabel;
	private IFactory factoryReference;
	private UpgradePopupPanel upgradePopupPanel;
	private UpgradeDialog upgradeDialog;
	private int number;
	public int Number {get; set;}
	public string Resource {get; set;}
	[Signal]
	public delegate void OnFactorySetEventHandler(int num);


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		upgradePopupPanel = GetNode<UpgradePopupPanel>("PopupPanel");
		upgradePopupPanel.Visible = false;
		upgradeDialog = GetNode<UpgradeDialog>("PopupPanel/UpgradeDialog");
		factoryNumberLabel = GetNode<Label>("factoryNumberLabel");
		factoryResourceLabel = GetNode<Label>("resourceNameLabel");
		upgradeDialog.OnClosedPanel += upgradePopupPanel.OnClosedHandler;
		this.OnFactorySet += upgradeDialog.OnFactorySetHandler;
		this.OnFactorySet += upgradePopupPanel.OnFactorySetHandler;
	}

	public void SetFactory(ref IFactory factory){
		factoryReference = factory;
	}

	public void SetFactoryNumber(int number){
		Number = number;
		if(factoryNumberLabel == null){
			factoryNumberLabel = GetNode<Label>("factoryNumberLabel");
		}
		if(upgradePopupPanel == null){
			upgradePopupPanel = GetNode<UpgradePopupPanel>("PopupPanel");
		}
		if(upgradeDialog == null){
			upgradeDialog = GetNode<UpgradeDialog>("PopupPanel/UpgradeDialog");
		}

		factoryNumberLabel.Text = "Factory #: " + Number;
		upgradePopupPanel.SetFactoryNumber(number);


		upgradeDialog.SetFactoryNumber(number);
		EmitSignal("OnFactorySet", number);
	}

	public void SetFactoryResource(string resource){
		Resource = this.factoryReference.AssignResource.Name;

		if(factoryResourceLabel == null){
			factoryResourceLabel = GetNode<Label>("resourceNameLabel");
		}

		factoryResourceLabel.Text = "Product: " + resource;
	}

	private void _on_viewbtn_pressed(){
		upgradePopupPanel.Visible = !upgradePopupPanel.Visible;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
