using System;

namespace Common.Processing.Interfaces {
    public interface ITestingView {
        void SetTestingMenuEnabled(bool value);
        event EventHandler<EventArgs> TestingDataCreation;
    }
}
