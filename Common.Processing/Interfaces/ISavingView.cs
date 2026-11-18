using System;

namespace Common.Processing.Interfaces {
    public interface ISavingView
    {
        void ShowStatusMessage(string message);
        event EventHandler<EventArgs> DataSaving;
    }
}
