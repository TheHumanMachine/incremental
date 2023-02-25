public class IronIngot : ITransformable{
    public Ingot Ingot { get; init; }
    private string name = "IronIngot";
    public string Name { get =>  name;  }

    public IronIngot(){
        Ingot = Ingot.Iron;
    }
}