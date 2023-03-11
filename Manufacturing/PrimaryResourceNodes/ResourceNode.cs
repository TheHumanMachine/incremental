public class ResourceNode{
    
    public int Supply { get; private set; }
    private PrimaryResourceFrame resource;

    private int defaultExtractAmount = 15;
    
    public ResourceNode(PrimaryResourceFrame resource, int supply){
        this.resource = resource;
        Supply = supply;
    }

    public PrimaryResourcePayload Extract(int extractedAmount){
        Supply -= extractedAmount;
        return new PrimaryResourcePayload(resource, extractedAmount);
    }
}