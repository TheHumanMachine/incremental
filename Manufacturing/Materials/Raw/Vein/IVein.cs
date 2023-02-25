public interface IVein{

    IRawResource Resource { get; }
    RawPayload Extract(int amount);
}