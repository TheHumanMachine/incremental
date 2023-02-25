using Godot;
using System.Collections.Generic;


public partial class main : Control
{
	// Called when the node enters the scene tree for the first time.
	
	private IronOreVein oreVein;
	private Label ironOreLabel;
	private Label ironIngotLabel; 

	IDictionary<Ore, int> oreContainer = new Dictionary<Ore, int>();
	IDictionary<Ingot, int> ingotContainer = new Dictionary<Ingot, int>();

	private Extractor extractor = new Extractor();
	//private IronOreTransformer ironTransformer = new IronOreTransformer(new IngotTransformerStrategy(0.5));
	
	private IronOreTransformer ironTransformer = new IronOreTransformer(new IngotTransformerStrategy(0.5));

	public override void _Ready()
	{
		oreVein = new IronOreVein();
		ironOreLabel = GetNode<Label>("background/ironOrelabel");
		ironOreLabel.Text = "Iron ore: 0";

		ironIngotLabel = GetNode<Label>("background/ironIngotLabel");
		ironIngotLabel.Text = "Iron ingot: 0";
	
		extractor.SetSupplySource(oreVein);
		extractor.OnOreExtracted += onOreExtracted;

		ironTransformer.
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	private void _on_button_pressed(){
		extractor.Extract();
	}

	private void _on_smelt_ironbtn_pressed(){
		int transformSupply = oreContainer[Ore.Iron];
		GD.Print("transform supply: " + transformSupply);

		ironTransformer.Transform(Ore.Iron, transformSupply);
	}

	private void onOreTransformed(Ingot ingotType, int originalOreSupply, int transformedSupply){
		if(transformedSupply > 0){
			oreContainer[Ore.Iron] -= originalOreSupply;
		}

		if(ingotContainer.ContainsKey(ingotType)){
			int amt = ingotContainer[ingotType];
			amt += transformedSupply;
			ingotContainer[ingotType] = amt;

		}else{
			ingotContainer[ingotType] = transformedSupply;
		}

		ironIngotLabel.Text = "Iron ingot: " + ingotContainer[ingotType];
		ironOreLabel.Text = "Iron ore: " + oreContainer[Ore.Iron];
	}

	private void onOreExtracted(Ore oreType, int extracted){
		if(oreContainer.ContainsKey(oreType)){
			int amt = oreContainer[oreType];
			amt += extracted;
			oreContainer[oreType] = amt;

		}else{
			oreContainer[oreType] = extracted;
		}

		ironOreLabel.Text = "Iron ore: " + oreContainer[oreType];
	}
}
