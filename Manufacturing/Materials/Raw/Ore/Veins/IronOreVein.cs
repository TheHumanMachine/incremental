public class IronOreVein : IVein
{

    private int supply = 100_000;
    private IRawResource resource = new IronOre();
    public IRawResource Resource { get =>  this.resource;  }

    public IronOreVein(int supply){
        this.supply = supply;
    }

    public IronOreVein(){
    }

    public int Supply{
        get { return supply; } 
    }

    public RawPayload Extract(int amount)
    {
        return new RawPayload(Resource, amount);
    }

}