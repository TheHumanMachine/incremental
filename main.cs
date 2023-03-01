using Godot;
using System.Collections.Generic;


public partial class main : Control
{
	// Called when the node enters the scene tree for the first time.
	
	private IronOreVein oreVein;
	private Label ironOreLabel;
	private Label ironIngotLabel; 

	IDictionary<string, int> oreContainer = new Dictionary<string, int>();
	IDictionary<string, int> ingotContainer = new Dictionary<string, int>();

	private Extractor extractor = new Extractor();
	
	private IronOreTransformer ironTransformer = new IronOreTransformer(new IngotTransformerStrategy(0.5));
	private ResourceLookup loopUpTable = ResourceLookup.Instance;

	public override void _Ready()
	{
		oreVein = new IronOreVein();
		ironOreLabel = GetNode<Label>("background/ironOrelabel");
		ironOreLabel.Text = "Iron ore: 0";

		ironIngotLabel = GetNode<Label>("background/ironIngotLabel");
		ironIngotLabel.Text = "Iron ingot: 0";
	
		extractor.SetSupplySource(oreVein);

		var temp = ResourceLookup.GetResourceFrame(10);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	private void _on_button_pressed(){
		var temp = extractor.Extract();
		extractorhandler(temp);
	}

	private void _on_smelt_ironbtn_pressed(){
		int transformSupply = oreContainer[new IronOre().Name];

		var payload = ironTransformer.Transform(Ore.Iron, transformSupply);

		transformHandler(payload);
	}

	private void extractorhandler(RawPayload payload){
		string materialKey = payload.Material.Name;
		if(oreContainer.ContainsKey(materialKey)){
			int amt = oreContainer[materialKey];
			amt += payload.Supply;
			oreContainer[materialKey] = amt;

		}else{
			oreContainer[materialKey] = payload.Supply;
		}

		ironOreLabel.Text = "Iron ore: " + oreContainer[materialKey];
	}

	private void transformHandler(TransformerPayload payload){
		string oreName = new IronOre().Name;
		if(payload.Payload > 0){
			oreContainer[oreName] -= payload.OriginalSupply;
		}

		var material = payload.Material.Name;
		if(ingotContainer.ContainsKey(material)){
			int amt = ingotContainer[material];
			amt += payload.Payload;
			ingotContainer[material] = amt;

		}else{
			ingotContainer[material] = payload.Payload;
		}

		ironIngotLabel.Text = "Iron ingot: " + ingotContainer[material];
		ironOreLabel.Text = "Iron ore: " + oreContainer[oreName];
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
