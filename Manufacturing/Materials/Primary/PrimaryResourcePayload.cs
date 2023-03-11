public struct PrimaryResourcePayload
{
    // easy way to pass around resources
    public int Supply { get; init; }
    public PrimaryResourceFrame Resource { get; init; }

    public PrimaryResourcePayload(PrimaryResourceFrame resource, int supply)
    {
        Resource = resource;
        Supply = supply;
    }
}