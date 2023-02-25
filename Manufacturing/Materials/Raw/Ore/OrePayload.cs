// Easy way to pass around how much ore is being processed
public struct OrePayload
{
    public int Supply { get; init; }
    public Ore OreType { get; init; }

    public OrePayload(Ore oreType, int supply)
    {
        OreType = oreType;
        Supply = supply;
    }
}