using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// Responsible for looking up resources

// I need to know what inputs this product takes
// input be product id
// should return input array of ids of given inputs
// ]

public sealed class ResourceLookup
{
    private static ResourceLookup instance = null;
    private static readonly object padlock = new object();
    private static IDictionary<int, ResourceFrame> resourceFrames = new Dictionary<int, ResourceFrame>();
    


    private ResourceLookup()
    {
        try
        {
            string text = File.ReadAllText("ProductData/manufacturingProducts.json");
            ResourceFrameRoot myDeserializedClass = JsonSerializer.Deserialize<ResourceFrameRoot>(text);
            
            // populate dictionary with product frames for easier access
            foreach(var prod in myDeserializedClass.products){
                resourceFrames[prod.Id] = prod;
            }
        }
        catch (Exception e)
        {
            if (e.InnerException != null)
            {
                string err = e.InnerException.Message;
                GD.Print(err);
            }
        }
    }

    public static ResourceLookup Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ResourceLookup();
                }
                return instance;
            }
        }
    }

    public static ResourceFrame? GetResourceFrame(int id){
        ResourceFrame frame;
        bool keyExists = resourceFrames.TryGetValue(id, out frame);
        if(keyExists){
            return frame;
        }else{
            return null;
        }
    }
}