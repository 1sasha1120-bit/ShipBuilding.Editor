using Common.Processing.Events;
using System;

namespace Common.Processing.Interfaces {
    public interface IEditingView<TModel> : IView {
        void SetModel(TModel model);

        void ShowErrorMessage(string message);

        event EventHandler<CrudActionEventArgs> CrudAction;
    }
}
