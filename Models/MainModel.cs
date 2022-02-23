using AviUtlExoEditor.LabelCut.Models.Item;
using Livet;

namespace AviUtlExoEditor.LabelCut.Models
{
    public class MainModel : NotificationObject
    {
        #region Property

        private ExoItem _OrgExoItem;

        public ExoItem OrgExoItem
        {
            get
            { return _OrgExoItem; }
            set
            { 
                if (_OrgExoItem == value)
                    return;
                _OrgExoItem = value;
                RaisePropertyChanged();
            }
        }

        private LabelItem[] _LabelItems;

        public LabelItem[] LabelItems
        {
            get
            { return _LabelItems; }
            set
            { 
                if (_LabelItems == value)
                    return;
                _LabelItems = value;
                RaisePropertyChanged();
            }
        }

        private ExoItem _NewExoItem;

        public ExoItem NewExoItem
        {
            get
            { return _NewExoItem; }
            set
            { 
                if (_NewExoItem == value)
                    return;
                _NewExoItem = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Method

        public void LoadExo(string filePath)
        {
            OrgExoItem = DAO.ExoAccessObject.Deserialize(filePath);
        }

        public void LoadLabel(string filePath)
        {
            LabelItems = DAO.LabelAccessObject.Deserialize(filePath);
        }

        public void Convert()
        {
            NewExoItem = AutoCutEditLogic.AutoCutEdit(OrgExoItem, LabelItems);
        }

        public void Save(string filePath)
        {
            DAO.ExoAccessObject.Serialize(NewExoItem, filePath);
        }

        #endregion
    }
}
