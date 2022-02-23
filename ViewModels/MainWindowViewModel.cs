using AviUtlExoEditor.LabelCut.Models;
using AviUtlExoEditor.LabelCut.Models.Item;
using Livet;
using Livet.Commands;
using Livet.EventListeners;

namespace AviUtlExoEditor.LabelCut.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region Ctor

        public MainWindowViewModel()
        {
            _model = new MainModel();
            CompositeDisposable.Add(new PropertyChangedEventListener(_model)
            {
                { nameof(_model.OrgExoItem), (s,e) =>
                {
                    RaisePropertyChanged(nameof(OrgExoItem));
                    ConvertCommand.RaiseCanExecuteChanged();
                }},
                { nameof(_model.LabelItems), (s,e) =>
                {
                    RaisePropertyChanged(nameof(LabelItems));
                    ConvertCommand.RaiseCanExecuteChanged();
                }},
                { nameof(_model.NewExoItem), (s,e) =>
                {
                    RaisePropertyChanged(nameof(NewExoItem));
                    SaveCommand.RaiseCanExecuteChanged();
                }},
            });
        }

        #endregion

        #region Property

        private string _ExoFile;

        public string ExoFile
        {
            get
            { return _ExoFile; }
            set
            {
                if (_ExoFile == value)
                    return;
                _ExoFile = value;
                RaisePropertyChanged();
                LoadExoCommand.RaiseCanExecuteChanged();
            }
        }

        private string _LabelFile;

        public string LabelFile
        {
            get
            { return _LabelFile; }
            set
            {
                if (_LabelFile == value)
                    return;
                _LabelFile = value;
                RaisePropertyChanged();
                LoadLabelCommand.RaiseCanExecuteChanged();
            }
        }

        public ExoItem OrgExoItem { get => _model.OrgExoItem; }

        public LabelItem[] LabelItems { get => _model.LabelItems; }

        public ExoItem NewExoItem { get => _model.NewExoItem; }

        #endregion

        #region Command

        private ViewModelCommand _LoadExoCommand;

        public ViewModelCommand LoadExoCommand
        {
            get
            {
                if (_LoadExoCommand == null)
                {
                    _LoadExoCommand = new ViewModelCommand(LoadExo, CanLoadExo);
                }
                return _LoadExoCommand;
            }
        }

        public bool CanLoadExo()
        {
            return !string.IsNullOrEmpty(ExoFile);
        }

        public void LoadExo()
        {
            _model.LoadExo(ExoFile);
        }

        private ViewModelCommand _LoadLabelCommand;

        public ViewModelCommand LoadLabelCommand
        {
            get
            {
                if (_LoadLabelCommand == null)
                {
                    _LoadLabelCommand = new ViewModelCommand(LoadLabel, CanLoadLabel);
                }
                return _LoadLabelCommand;
            }
        }

        public bool CanLoadLabel()
        {
            return !string.IsNullOrEmpty(LabelFile);
        }

        public void LoadLabel()
        {
            _model.LoadLabel(LabelFile);
        }

        private ViewModelCommand _ConvertCommand;

        public ViewModelCommand ConvertCommand
        {
            get
            {
                if (_ConvertCommand == null)
                {
                    _ConvertCommand = new ViewModelCommand(_model.Convert, CanConvert);
                }
                return _ConvertCommand;
            }
        }

        public bool CanConvert()
        {
            return OrgExoItem != null && LabelItems != null;
        }


        private ViewModelCommand _SaveCommand;

        public ViewModelCommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new ViewModelCommand(Save, CanSave);
                }
                return _SaveCommand;
            }
        }

        public bool CanSave()
        {
            return NewExoItem != null;
        }

        public void Save()
        {
            Livet.Messaging.IO.SavingFileSelectionMessage message = new("SaveAs");
            Messenger.Raise(message);

            if (message.Response == null || message.Response.Length < 1 || string.IsNullOrEmpty(message.Response[0])) return;

            _model.Save(message.Response[0]);
        }

        #endregion

        #region Field

        private MainModel _model;

        #endregion
    }
}
