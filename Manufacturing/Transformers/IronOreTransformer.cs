using Godot;

public partial class IronOreTransformer : Node, IOreTransformer
{

    [Signal]
    public delegate void OnOreTransformedEventHandler(Ingot ore, int payload, int transformedSupply);
    private IProductTransformerStrategy strategy;

    public IronOreTransformer(IProductTransformerStrategy strategy){
        this.strategy = strategy;
    }

    public TransformerPayload Transform(Ore ore, int supply)
    {
        int payload = strategy.Transform(ore, supply);

        EmitSignal("OnOreTransformed", (int)Ingot.Iron, payload, supply);

        return new TransformerPayload(new IronIngot(), payload, supply);
    }
}