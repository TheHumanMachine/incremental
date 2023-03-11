using System.Collections.Generic;
// generated from json to c#

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

    public class Input
    {
        public string name { get; set; }
        public int cost { get; set; }
    }


public class PrimaryResourceFrame : IResourceFrame
    {
        public string Name { get; init; }
        public List<Input> inputs { get; init; }
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
    public List<Input> inputs { get; init; }
}

public interface IResourceFrame {
    string Name { get; init; }
    List<Input> inputs { get; init; }
}