using Godot;
using System.Collections.Generic;


public partial class main : Control
{
	// Called when the node enters the scene tree for the first time.
	private PackedScene factoryElementScene = GD.Load<PackedScene>("res://scenes/FactoryElement.tscn");
	private VBoxContainer factoryContainer;
	private Label ironOreLabel;
	private Label ironIngotLabel;
	private Label moneyLabel;
	private Label dayElapsedLabel;
	private Label factoryPriceLabel;
	private Label factoryCountLabel;

	private double money = 100; 

	IDictionary<string, int> primaryResourcesContainer = new Dictionary<string, int>();
	IDictionary<string, int> secondaryResourcesContainer = new Dictionary<string, int>();
	private ResourceLookup loopUpTable = ResourceLookup.Instance;

	private Extractor extractor;
	
	private IronOreTransformer ironTransformer = new IronOreTransformer(new IngotTransformerStrategy(0.5));
	private SteelTransformer steelTransformer;

	private SteelTransformer steelTransformer2;
	private SteelTransformer steelTransformer3;
	private SteelTransformer steelTransformer4;

	private List<IFactory> factories = new List<IFactory>();

	private int currentFactoryPrice = 100;
	private double factoryPriceIncreasePercentage = 0.25;

	private ManufacturingUnit manufUnit;

	private Timer timer;

	private int day = 1;

	SecondaryResourceFrame steelFrame;

	public override void _Ready()
	{
		factoryContainer = GetNode<VBoxContainer>("background/MarginContainer/Panel/VBoxContainer");

		ironOreLabel = GetNode<Label>("background/ironOrelabel");
		ironOreLabel.Text = "Iron ore: 0";

		ironIngotLabel = GetNode<Label>("background/ironIngotLabel");
		ironIngotLabel.Text = "Steel coils: 0";

		moneyLabel = GetNode<Label>("background/moneyLabel");
		moneyLabel.Text = "$: 0";

		dayElapsedLabel = GetNode<Label>("background/dayElapsedLabel");
		dayElapsedLabel.Text = "Day: " + day;

		factoryPriceLabel = GetNode<Label>("background/factoryPriceLabel");
		UpdateFactoryPriceLabel();

		factoryCountLabel = GetNode<Label>("background/factoryCountLabel");
		UpdateFactoryCountLabel();

		ResourceNode resourceNode = new ResourceNode((PrimaryResourceFrame)ResourceLookup.GetResourceFrame(0), 100_000);

		extractor = new Extractor(resourceNode);
		
		steelFrame = (SecondaryResourceFrame)ResourceLookup.GetResourceFrame(3);

		steelTransformer= new SteelTransformer(steelFrame);
		steelTransformer2 = new SteelTransformer(steelFrame);
		steelTransformer3 = new SteelTransformer(steelFrame);
		steelTransformer4 = new SteelTransformer(steelFrame);

		manufUnit = new ManufacturingUnit(steelFrame);

		manufUnit.AddTransformer(steelTransformer2);
		manufUnit.AddTransformer(steelTransformer3);
		manufUnit.AddTransformer(steelTransformer4);


		timer = new Timer();

		this.AddChild(timer);
		timer.WaitTime = 2;
		timer.Timeout += _on_Timer_timeout;
		timer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_Timer_timeout(){
		day++;
		dayElapsedLabel.Text = "Day: " + day;
		ProcessFactories();
	}


	private void ProcessFactories(){
		if(factories.Count > 0){
			foreach(var factory in factories){
				var payload = factory.GetOutput();
				AddSecondaryPayloadToInventory(payload);
			}
			UpdateMoneyLabelAmount();
			UpdateSteelLabelAmount();
		}
	}

	private void AddSecondaryPayloadToInventory(SecondaryResourcePayload payload){
		string materialKey = payload.Resource.Name;
		if(secondaryResourcesContainer.ContainsKey(materialKey)){
			int amt = secondaryResourcesContainer[materialKey];
			amt += payload.Supply;
			secondaryResourcesContainer[materialKey] = amt;
		}else{
			secondaryResourcesContainer[materialKey] = payload.Supply;
		}
	}

	private void RemoveSecondaryResourceToInventory(SecondaryResourcePayload payload){
		string materialKey = payload.Resource.Name;
		if(secondaryResourcesContainer.ContainsKey(materialKey)){
			secondaryResourcesContainer[materialKey] -= payload.Supply;

			if(secondaryResourcesContainer[materialKey] < 0){
				secondaryResourcesContainer[materialKey] = 0;
			}
		}
	}

	private int GetSecondaryResourceCount(string materialKey){

		if(secondaryResourcesContainer.ContainsKey(materialKey)){
			return secondaryResourcesContainer[materialKey];
		}
		return 0;
	}

	private void _on_button_pressed(){
		var temp = extractor.Extract();
		extractorhandler(temp);
	}

	private void _on_smelt_ironbtn_pressed(){
		int transformSupply = primaryResourcesContainer["ore.iron"];
		primaryResourcesContainer["ore.iron"] -= transformSupply;

		UpdateIronOreLabelAmount();
		//var payload = ironTransformer.Transform(Ore.Iron, transformSupply);

		//transformHandler(payload);
		var payload = steelTransformer.Transform(transformSupply);
		transformHandler(payload);
	}

	private void extractorhandler(PrimaryResourcePayload payload){
		string materialKey = payload.Resource.Name;

		if(primaryResourcesContainer.ContainsKey(materialKey)){
			int amt = primaryResourcesContainer[materialKey];
			amt += payload.Supply;
			primaryResourcesContainer[materialKey] = amt;

		}else{
			primaryResourcesContainer[materialKey] = payload.Supply;
		}

		UpdateIronOreLabelAmount();
	}

	private void UpdateIronOreLabelAmount(){
		ironOreLabel.Text = "Iron ore: " + primaryResourcesContainer["ore.iron"];
	}
	
	private void UpdateSteelLabelAmount(){
		ironIngotLabel.Text = "Steel Coils: " + secondaryResourcesContainer["metal.steel"];
	}

	private void UpdateMoneyLabelAmount(){
		moneyLabel.Text = "$: " + money;
	}

	private void UpdateFactoryPriceLabel(){ 
		factoryPriceLabel.Text = "Factory Price: " + currentFactoryPrice;
	}

	private void UpdateFactoryCountLabel(){
		factoryCountLabel.Text = "Factory Count: " + factories.Count;
	}

	private void transformHandler(SecondaryResourcePayload payload){
		AddSecondaryPayloadToInventory(payload);
		UpdateSteelLabelAmount();
	}

	private void _on_sell_steelbtn_pressed(){
		int sellAmount = 0;

		sellAmount = GetSecondaryResourceCount(steelFrame.Name);
		RemoveSecondaryResourceToInventory(new SecondaryResourcePayload(steelFrame, sellAmount));
		money += sellAmount * 4.0;

		UpdateMoneyLabelAmount();
		UpdateSteelLabelAmount();
	}

	private void _on_buy_factorybtn_pressed(){
		if(money >= currentFactoryPrice){
			money -= currentFactoryPrice;
			IFactory temp = new SimpleFactory(manufUnit);
			factories.Add(temp);

			double nPrice = currentFactoryPrice + currentFactoryPrice * factoryPriceIncreasePercentage;
			currentFactoryPrice = Mathf.RoundToInt(nPrice);

			FactoryElement instance = (FactoryElement)factoryElementScene.Instantiate();
			
			instance.SetFactory(ref temp);
			instance.SetFactoryNumber(factories.Count);
			instance.SetFactoryResource("metal.steel");
			factoryContainer.AddChild(instance);

			UpdateMoneyLabelAmount();
			UpdateFactoryCountLabel();
			UpdateFactoryPriceLabel();
		}
	}

}
