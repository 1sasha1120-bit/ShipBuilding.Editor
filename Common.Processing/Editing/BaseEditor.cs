using Common.Processing.Events;
using Common.Processing.Interfaces;
using Common.Processing.Types;
using System;

namespace Common.Processing.Editing {
    public abstract class BaseEditor<TModel> : IEditor<TModel> {

        public void RunAction(CrudMode mode, TModel model) {
            switch (mode) {
                case CrudMode.Creation: Create(model); break;
                case CrudMode.Updating: Update(model); break;
                case CrudMode.Deleting: Delete(model); break;
            }
        }

        public event EventHandler<StringEventArgs>? ActionError;

        protected void OnActionError(StringEventArgs eventArgs) {
            ActionError?.Invoke(this, eventArgs);
        }

        public event EventHandler<StringEventArgs>? DataChanged;

        protected void OnDataChanged(StringEventArgs eventArgs) {
            DataChanged?.Invoke(this, eventArgs);
        }

        private void Create(TModel model) {
            if (!CheckCreation(model, out string? message)) {
                OnActionError(new StringEventArgs(message));
                return;
            }
            DoCreate(model);
            OnDataChanged(new StringEventArgs("Дані додано"));
        }

        protected abstract bool CheckCreation(TModel model, out string? message);
        protected abstract void DoCreate(TModel model);

        private void Update(TModel model) {
            if (!CheckUpdating(model, out string? message)) {
                OnActionError(new StringEventArgs(message));
                return;
            }
            DoUpdate(model);
            OnDataChanged(new StringEventArgs("Дані оновлено"));
        }

        protected abstract bool CheckUpdating(TModel model, out string? message);
        protected abstract void DoUpdate(TModel model);

        private void Delete(TModel model) {
            if (!CheckDeleting(model, out string? message)) {
                OnActionError(new StringEventArgs(message));
                return;
            }
            DoDelete(model);
            OnDataChanged(new StringEventArgs("Дані видалено"));
        }

        protected abstract bool CheckDeleting(TModel model, out string? message);
        protected abstract void DoDelete(TModel model);


    }
}
