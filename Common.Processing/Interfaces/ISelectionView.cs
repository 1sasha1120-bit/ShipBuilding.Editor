using Common.Processing.Events;
using System;

namespace Common.Processing.Interfaces {
    public interface ISelectionView {
        void SetKeys(string[] keys);
        event EventHandler<StringEventArgs> KeySelected;
    }
}
