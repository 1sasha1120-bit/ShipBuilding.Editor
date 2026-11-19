using Common.Processing.Interfaces;
using Common.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using static Common.Settings.CommonTexts;

namespace Common.Processing.Editing {
    public abstract class BaseViewsController {

        private List<ISavingView> _savingViews = new List<ISavingView>();
        private List<ITestingView> _testingViews = new List<ITestingView>();
        private List<IInfoView> _infoViews =  new List<IInfoView>();

        public Func<bool>? CreateTestingData;

        public void CheckAndAddViews(params object[] objects) {
            foreach (object obj in objects) {
                if (obj is ISavingView) {
                    AddSavingView((ISavingView)obj);
                }
                if (obj is ITestingView) {
                    AddTestingView((ITestingView)obj);
                }
                if (obj is IInfoView) {
                    AddInfoView((IInfoView)obj);
                }
            }
        }

        private void AddSavingView(ISavingView savingView) {
            savingView.DataSaving += SavingView_DataSaving;
            _savingViews.Add(savingView);
        }

        private void SavingView_DataSaving(object? sender, EventArgs e) {
            Save();
            if (sender is not ISavingView view)
                return;

            view.ShowStatusMessage(DataSavedMessage);

        }

        protected abstract void Save();

        void AddTestingView(ITestingView testingView) {
            testingView.TestingDataCreation += TestingView_TestingDataCreation;
            _testingViews.Add(testingView);
        }

        public event EventHandler<EventArgs>? DataChanged;

        protected void OnDataChanged(EventArgs eventArgs) {
            DataChanged?.Invoke(this, eventArgs);
        }

        private void TestingView_TestingDataCreation(object? sender, EventArgs e) {
            if (CreateTestingData?.Invoke() == true) {
                UpdateViewsState();
                OnDataChanged(EventArgs.Empty);
            }
        }

        private void AddInfoView(IInfoView infoView) {
            _infoViews.Add(infoView);
        }

        protected abstract bool GetStorageIsEmpty();
        protected abstract string GetStorageDataString();

        public void UpdateViewsState() {
            SetInfo(GetStorageDataString());
            SetTestingMenuEnabled(GetStorageIsEmpty());
        }

        private void SetInfo(string info) {
            foreach(var infoView in _infoViews) {
                infoView.UpdateInfo(info);
            }
        }

        private void SetTestingMenuEnabled(bool value) {
            foreach(var testingView in _testingViews) {
                testingView.SetTestingMenuEnabled(value);
            }
        }
    }
}
