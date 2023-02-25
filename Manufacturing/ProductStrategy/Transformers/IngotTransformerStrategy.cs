using Godot;

public class IngotTransformerStrategy : IProductTransformerStrategy
{
    double efficiency = 1.0;

    public IngotTransformerStrategy(double efficiency){
        this.efficiency = efficiency;
    }

    public IngotTransformerStrategy(){
    }

    public int Transform(Ore ore, int supply)
    {
        // determine what inputs are required for this transform
        return Mathf.RoundToInt(supply * efficiency);
    }
}