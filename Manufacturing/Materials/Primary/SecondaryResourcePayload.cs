public struct SecondaryResourcePayload
{
    // easy way to pass around resources
    public int Supply { get; init; }
    public SecondaryResourceFrame Resource { get; init; }

    public SecondaryResourcePayload(SecondaryResourceFrame resource, int supply)
    {
        Resource = resource;
        Supply = supply;
    }
}