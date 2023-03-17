public interface IManufacturingUnit {

    public SecondaryResourceFrame AssignedResource {get;init;}
    void AddTransformer(ITransformer transformer);
    void AddManufacturingUpdate(UpgradeSlot slot, IUpgrade upgrade);
    SecondaryResourcePayload GetOutput();
    
}

public enum UpgradeSlot{
    One,
    Two,
    Three
}