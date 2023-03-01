using System.Collections.Generic;

public struct ResourceFrame{
        public string Name { get; init; }
        public int Id { get; init; }
        public List<int> Inputs { get; init; }
        public int Output { get; init; }
}

public class ResourceFrameRoot
{
    public List<ResourceFrame> products { get; set; }
}