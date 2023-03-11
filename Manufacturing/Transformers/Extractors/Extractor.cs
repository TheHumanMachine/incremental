using Godot;
using System;

public partial class Extractor : Node{

    private float efficiency = 0.8f;
    private int maxCapacity = 1;

    private ResourceNode resourceNode;

    public Extractor(ResourceNode resourceNode){
        this.resourceNode = resourceNode;
    }

    public Extractor(){}

    public PrimaryResourcePayload Extract(){
        return Extract(maxCapacity);
    }

    private PrimaryResourcePayload Extract(int amount){
        var payload = this.resourceNode.Extract(amount);
        int extracted = (int)Mathf.Round(payload.Supply * efficiency);
        return new PrimaryResourcePayload(payload.Resource, extracted);
    }
}