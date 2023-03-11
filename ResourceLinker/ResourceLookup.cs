using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;



// On init set the Transformer to the resource it will be

public sealed class ResourceLookup
{
    private static ResourceLookup instance = null;
    private static readonly object padlock = new object();
    private static IDictionary<int, IResourceFrame> resourceFrames = new Dictionary<int, IResourceFrame>();

    private static IDictionary<string, int> nameToIDTable = new Dictionary<string, int>();

    private static IDictionary<int, List<int>> idToInputTable = new Dictionary<int, List<int>>(); 
    

    private ResourceLookup()
    {
        try
        {
            string text = File.ReadAllText("ProductData/manufacturingProducts.json");
            ResourceFrameRoot root = JsonSerializer.Deserialize<ResourceFrameRoot>(text);
            // process primary resource frames
            var primaryProducts = root.products[0].primary;

            for(int i = 0; i < primaryProducts.Count; i++){
                var prod = primaryProducts[i];
                
                nameToIDTable[prod.Name] = i;

                resourceFrames[i] = prod;
            }

            // process secondary resource frames
            var offset = primaryProducts.Count;
            var secondaryProducts = root.products[0].secondary;

            for(int i = 0; i < secondaryProducts.Count; i++){
                var prod = secondaryProducts[i];
                
                nameToIDTable[prod.Name] = i + offset;

                resourceFrames[i + offset] = prod;
            }


            foreach (KeyValuePair<string, int> kvp in nameToIDTable)
            {
                //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                GD.Print(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
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

    public static int GetIdByName(string name){
        int id;
        bool keyExists = nameToIDTable.TryGetValue(name, out id);
        if(keyExists){
            return id;
        }else{
            return -1;
        }
    }

    public static IResourceFrame GetResourceFrame(int id){
        IResourceFrame frame;
        bool keyExists = resourceFrames.TryGetValue(id, out frame);
        if(keyExists){
            return frame;
        }else{
            return null;
        }
    }
}