public interface IOreVein{
    Ore OreType();
    OrePayload Extract(int amount);
}