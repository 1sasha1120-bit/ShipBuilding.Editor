using Common.Processing.Events;
using Common.Processing.Types;
using System;

namespace Common.Processing.Interfaces {
    public interface IEditor<TModel> {
        void RunAction(CrudMode mode, TModel model);

        event EventHandler<StringEventArgs> ActionError;
        event EventHandler<StringEventArgs> DataChanged;
    }
}
