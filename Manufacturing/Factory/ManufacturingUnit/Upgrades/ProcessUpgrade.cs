using System;
using Godot;

public class ProcessUpgrade : IUpgrade
{
    private double efficiencyModifier = 1.5;
    public void ModifyInput(ref int input)
    {
        input = Convert.ToInt32(input * efficiencyModifier);
    }
}