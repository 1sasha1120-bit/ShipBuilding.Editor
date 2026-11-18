using System;

namespace Common.Processing.Events {
    public class StringEventArgs : EventArgs {
        public readonly string? Value;

        public StringEventArgs(string? value) {
            Value = value;
        }
    }
}
