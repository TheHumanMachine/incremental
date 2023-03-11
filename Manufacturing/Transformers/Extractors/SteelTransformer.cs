
using System.Collections.Generic;

public class SteelTransformer : ITransformer{
    
    private SecondaryResourceFrame resource;
    public SteelTransformer(SecondaryResourceFrame resource){
        this.resource = resource;
        // set the TYPE we are transfering too

        // Need to link resource supplies of specificed input ids

        // 
        
    }

    public SecondaryResourcePayload Transform(int supply){
        return new SecondaryResourcePayload(this.resource, Godot.Mathf.RoundToInt(supply * 0.5));
    }

}