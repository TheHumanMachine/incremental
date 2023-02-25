public class IronOreVein : IOreVein
{

    private int supply = 100_000;

    public IronOreVein(int supply){
        this.supply = supply;
    }

    public IronOreVein(){

    }

    public int Supply{
        get { return supply; } 
    }

    public OrePayload Extract(int amount)
    {
        throw new System.NotImplementedException();
    }

    public Ore OreType()
    {
        return Ore.Iron;
    }
}