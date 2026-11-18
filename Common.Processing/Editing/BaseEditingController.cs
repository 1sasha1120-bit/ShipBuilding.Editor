using Common.Processing.Events;
using Common.Processing.Interfaces;
using System;

namespace Common.Processing.Editing {
    public abstract class BaseEditingController<TModel> {

        protected IEditor<TModel> Editor { get; }
        protected ISelectionView SelectionView { get; }

        private readonly IEditingView<TModel> editingView;

        public BaseEditingController(IEditor<TModel> editor,
            ISelectionView selectionView, IEditingView<TModel> editingView) {

            this.Editor = editor
                ?? throw new ArgumentNullException(nameof(editor));
            this.SelectionView = selectionView
                ?? throw new ArgumentNullException(nameof(selectionView));
            this.editingView = editingView
                ?? throw new ArgumentNullException(nameof(editingView));
            SetHandlers();
        }

        private void SetHandlers() {
            SelectionView.KeySelected += SelectionView_KeySelected;
            editingView.CrudAction += EditingView_CrudAction;
            Editor.DataChanged += Editor_DataChanged;
            Editor.ActionError += Editor_ActionError;
        }

        private void Editor_ActionError(object? sender, StringEventArgs e)
        {
            editingView.ShowErrorMessage(e.Value!);
        }

        private void SelectionView_KeySelected(object? sender, StringEventArgs e) {
            string? key = e.Value;
            KeyToModel(key, out TModel model);
            editingView.SetModel(model);
        }

        protected abstract void KeyToModel(string? key, out TModel model);

        private void EditingView_CrudAction(object? sender, CrudActionEventArgs e) {
            Editor.RunAction(e.CrudMode, (TModel)e.KeyableObject);
        }

        public event EventHandler<EventArgs>? DataChanged;

        protected void OnDataChanged(EventArgs eventArgs) {
            DataChanged?.Invoke(this, eventArgs);
        }

        private void Editor_DataChanged(object? sender, EventArgs e) {
            UpdateViewsData();
            OnDataChanged(EventArgs.Empty);
        }

        public abstract void UpdateViewsData();

    }
}
