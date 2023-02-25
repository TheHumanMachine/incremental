using Godot;

public class IronOreTransformer : Node, IOreTransformer
{

    [Signal]
    public delegate void OnOreTransformedEventHandler(Ingot ingot, int originalOreSupply, int transformedSupply);
    private IProductTransformerStrategy strategy;

    public IronOreTransformer(IProductTransformerStrategy strategy){
        this.strategy = strategy;
    }

    public void Transform(Ore ore, int supply)
    {
        int ingotSupply = strategy.Transform(ore, supply);

        EmitSignal("OnOreTransformed", (int)Ingot.Iron, supply, ingotSupply);
    }
}