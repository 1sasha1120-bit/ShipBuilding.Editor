namespace Common.Processing.Interfaces
{
    public interface IView
    {
        string Name { get; set; }
        void ShowStatusMessage(string message);


    }
}
