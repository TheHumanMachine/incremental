public interface IFactory{

    public SecondaryResourceFrame AssignResource {get;}
    void SetManufacturingUnit(IManufacturingUnit unit);

    SecondaryResourcePayload GetOutput();
    
}