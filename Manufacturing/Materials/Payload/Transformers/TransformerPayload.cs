// Easy way to pass around how much ore is being processed
public struct TransformerPayload
{
    public int Payload { get; init; }
    public ITransformable Material{ get; init; }

    public int OriginalSupply { get; init; }

    public TransformerPayload(ITransformable material, int payload, int originalSupply)
    {
        Material = material;
        Payload = payload;
        OriginalSupply = originalSupply;
    }
}