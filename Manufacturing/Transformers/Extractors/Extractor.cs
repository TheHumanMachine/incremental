using Godot;
using System;

public partial class Extractor : Node{

    [Signal]
	public delegate void OnOreExtractedEventHandler(Ore oreType, int extracted);

    IOreVein supplySource;
    private float efficiency = 0.8f;
    private int maxCapacity = 1;

    public Extractor(){
    }

    public void SetSupplySource(IOreVein supplySource){
        this.supplySource = supplySource;
    }

    public void Extract(){
        Extract(maxCapacity);
    }

    private void Extract(int amount){
        var payload = this.supplySource.Extract(amount);

        int extracted = (int)Mathf.Round(payload.Supply * efficiency);
        
        //signal for how much we have extracted
        EmitSignal("OnOreExtracted", (int)supplySource.OreType(),  extracted);
    }
}