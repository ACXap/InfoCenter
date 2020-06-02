using GalaSoft.MvvmLight;

namespace Common.Data
{
    public abstract class TypeData : ViewModelBase
    {
        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private int _code = 1;
        public int Code
        {
            get => _code;
            set => Set(ref _code, value);
        }

        public abstract void Init(string str);

        public TypeData() { }
    }
}