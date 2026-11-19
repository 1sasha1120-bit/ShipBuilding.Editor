using Common.Interfaces;
using System;
using System.Collections.Generic;
using static Common.Settings.CommonTexts;

namespace Common.Data.IO {
    public class FileTypeSelector {

        protected FileTypeInformer[] _fileTypeInformers;
        private FileTypeInformer _currentInformer = null!;

        public FileTypeInformer CurrentInformer {
            get => _currentInformer;
            set {
                if (Array.IndexOf(_fileTypeInformers, value) < 0) {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _currentInformer = value;
            }
        }
        public Func<IEnumerable<IKeyable>, string, string?, bool, IKeyable?> KeyableSelector { get; set; }

        public FileTypeSelector(FileTypeInformer[] fileTypeInformers,
            Func<IEnumerable<IKeyable>, string, string?, bool, IKeyable?> keySelector) {

            //
            _fileTypeInformers = fileTypeInformers;
            CurrentInformer = _fileTypeInformers[0];
            KeyableSelector = keySelector;
        }

        public void Select() {
            //throw new NotImplementedException();
            IKeyable? selectedObj = KeyableSelector(_fileTypeInformers,
                SelectFileTypeCaption, CurrentInformer.Key, true);
            if (selectedObj is FileTypeInformer informer)
            {
                CurrentInformer = informer;
            }
        }

    }
}
