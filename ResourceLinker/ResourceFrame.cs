// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System.Collections.Generic;

public class PrimaryResourceFrame : IResourceFrame
    {
        public string Name { get; init; }
        public List<string> inputs { get; init; }
    }

public class Product
{
    public List<PrimaryResourceFrame> primary { get; init; }
    public List<SecondaryResourceFrame> secondary { get; init; }
}

public class ResourceFrameRoot
{
    public List<Product> products { get; init; }
}

public class SecondaryResourceFrame : IResourceFrame
{
    public string Name { get; init; }
    public List<string> inputs { get; init; }
}

public interface IResourceFrame {
    string Name { get; init; }
    List<string> inputs { get; init; }
}