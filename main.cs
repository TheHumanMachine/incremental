using Godot;
using System.Collections.Generic;


public partial class main : Control
{
	// Called when the node enters the scene tree for the first time.
	
	private IronOreVein oreVein;
	private Label ironOreLabel;
	private Label ironIngotLabel;
	private Label moneyLabel;
	private Label dayElapsedLabel;

	private double money = 0; 

	IDictionary<string, int> primaryResourcesContainer = new Dictionary<string, int>();
	IDictionary<string, int> secondaryResourcesContainer = new Dictionary<string, int>();

	private Extractor extractor;
	
	private IronOreTransformer ironTransformer = new IronOreTransformer(new IngotTransformerStrategy(0.5));
	private SteelTransformer steelTransformer;

	private ResourceLookup loopUpTable = ResourceLookup.Instance;

	private Timer timer;

	private int day = 1;

	public override void _Ready()
	{
		ironOreLabel = GetNode<Label>("background/ironOrelabel");
		ironOreLabel.Text = "Iron ore: 0";

		ironIngotLabel = GetNode<Label>("background/ironIngotLabel");
		ironIngotLabel.Text = "Steel coils: 0";

		moneyLabel = GetNode<Label>("background/moneyLabel");
		moneyLabel.Text = "$: 0";

		dayElapsedLabel = GetNode<Label>("background/dayElapsedLabel");
		dayElapsedLabel.Text = "Day: " + day;


		ResourceNode resourceNode = new ResourceNode((PrimaryResourceFrame)ResourceLookup.GetResourceFrame(0), 100_000);

		extractor = new Extractor(resourceNode);
		
		var steelFrame = (SecondaryResourceFrame)ResourceLookup.GetResourceFrame(3);
		steelTransformer= new SteelTransformer(steelFrame);

		timer = new Timer();

		this.AddChild(timer);
		timer.WaitTime = 2;
		timer.Timeout += _on_Timer_timeout;
		timer.Start();
		//timer.connect("timeout", self, "_on_Timer_timeout")
		//timer.start()

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void UpdateTimerLabel(){
		dayElapsedLabel.Text = "Day: " + day;
	}

	private void _on_Timer_timeout(){
		day++;
		dayElapsedLabel.Text = "Day: " + day;
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

	private void transformHandler(SecondaryResourcePayload payload){
		string materialKey = payload.Resource.Name;
		if(secondaryResourcesContainer.ContainsKey(materialKey)){
			int amt = secondaryResourcesContainer[materialKey];
			amt += payload.Supply;
			secondaryResourcesContainer[materialKey] = amt;
		}else{
			secondaryResourcesContainer[materialKey] = payload.Supply;
		}
		UpdateSteelLabelAmount();
	}

	private void _on_sell_steelbtn_pressed(){
		int sellAmount = 0;

		if(secondaryResourcesContainer.ContainsKey("metal.steel")){
			sellAmount = secondaryResourcesContainer["metal.steel"];
			secondaryResourcesContainer["metal.steel"] = 0;
		}

		money += sellAmount * 4.0;

		UpdateMoneyLabelAmount();
		UpdateSteelLabelAmount();
	}





	// private void onOreTransformed(Ingot ingotType, int originalOreSupply, int transformedSupply){
	// 	if(transformedSupply > 0){
	// 		oreContainer[Ore.Iron] -= originalOreSupply;
	// 	}

	// 	if(ingotContainer.ContainsKey(ingotType)){
	// 		int amt = ingotContainer[ingotType];
	// 		amt += transformedSupply;
	// 		ingotContainer[ingotType] = amt;

	// 	}else{
	// 		ingotContainer[ingotType] = transformedSupply;
	// 	}

	// 	ironIngotLabel.Text = "Iron ingot: " + ingotContainer[ingotType];
	// 	ironOreLabel.Text = "Iron ore: " + oreContainer[Ore.Iron];
	// }
}
