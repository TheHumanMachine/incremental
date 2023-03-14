public interface IFactory{

    void SetManufacturingUnit(IManufacturingUnit unit);

    SecondaryResourcePayload GetOutput();
    
}