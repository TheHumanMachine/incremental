public class SimpleFactory : IFactory
{
    private IManufacturingUnit manufacturingUnit;

    public SecondaryResourceFrame AssignResource {get; private set;}

    public SimpleFactory(IManufacturingUnit unit){
        this.manufacturingUnit = unit;
        AssignResource = this.manufacturingUnit.AssignedResource;
    }

    public SecondaryResourcePayload GetOutput()
    {
        return this.manufacturingUnit.GetOutput();
    }

    public void SetManufacturingUnit(IManufacturingUnit unit)
    {
        this.manufacturingUnit = unit;
        AssignResource = this.manufacturingUnit.AssignedResource;
    }
}