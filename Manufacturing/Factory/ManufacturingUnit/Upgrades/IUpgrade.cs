public interface IUpgrade{

    // Needs to be changed to use some form of like Manufacturing Struct
    // so we can pass in and change multiple different things without worrying
    void ModifyInput(ref int input);

}