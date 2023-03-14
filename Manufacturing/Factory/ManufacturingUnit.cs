using System.Collections.Generic;

public class ManufacturingUnit : IManufacturingUnit
{
    private List<ITransformer> transformers = new List<ITransformer>();
    private int defaultCapacity = 2;
    public SecondaryResourceFrame AssignedResource {get;init;}

    public IUpgrade SlotOne {get; private set;}
    public IUpgrade SlotTwo {get; private set;}
    public IUpgrade SlotThree {get; private set;}
    
    public ManufacturingUnit(SecondaryResourceFrame resource){
        AssignedResource = resource;
    }

    public void AddTransformer(ITransformer transformer)
    {
       this.transformers.Add(transformer);
    }

    public void AddManufacturingUpdate(UpgradeSlot slot, IUpgrade upgrade){
        switch(slot){
            case UpgradeSlot.One:
                SlotOne = upgrade;
                break;
            case UpgradeSlot.Two:
                SlotTwo = upgrade;
                break;
            case UpgradeSlot.Three:
                SlotThree = upgrade;
                break;
        }
    }

    public SecondaryResourcePayload GetOutput()
    {
        int supply = 0;
        if(transformers.Count > 0){
            foreach(var transformer in transformers){
                var payload = transformer.Transform(defaultCapacity);
                supply += payload.Supply;
            }
        }
        return new SecondaryResourcePayload(AssignedResource, supply);
    }
}