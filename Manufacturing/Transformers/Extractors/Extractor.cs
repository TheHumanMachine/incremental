using Godot;
using System;

public partial class Extractor : Node{


    IVein supplySource;
    private float efficiency = 0.8f;
    private int maxCapacity = 1;

    public Extractor(){
    }

    public void SetSupplySource(IVein supplySource){
        this.supplySource = supplySource;
    }

    public RawPayload Extract(){
        return Extract(maxCapacity);
    }

    private RawPayload Extract(int amount){
        var payload = this.supplySource.Extract(amount);

        int extracted = (int)Mathf.Round(payload.Supply * efficiency);
        return new RawPayload(supplySource.Resource, extracted);
    }
}