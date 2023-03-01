using System;
using Godot;

public sealed class ResourceLinker
{
    private static ResourceLinker instance = null;
    private static readonly object padlock = new object();


    private ResourceLinker()
    {
        try
        {

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

    public static ResourceLinker Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ResourceLinker();
                }
                return instance;
            }
        }
    }

    public static void EstablishResourceLink(){

    }
}