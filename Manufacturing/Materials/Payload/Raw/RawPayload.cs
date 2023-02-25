
// use to pass around 'raw' materials from veinsS
public struct RawPayload
{
    public int Supply { get; init; }
    public IRawResource Material{ get; init; }

    public RawPayload(IRawResource material, int supply)
    {
        Material = material;
        Supply = supply;
    }
}