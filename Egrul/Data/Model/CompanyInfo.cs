using GalaSoft.MvvmLight;

namespace Egrul.Data.Model
{
    public class CompanyInfo :ViewModelBase
    {
        public string Director { get; set; }
        public string CountRecords { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string FullTitle { get; set; }
        public string Ogrn { get; set; }
        public string Inn { get; set; }
        public string TokenLoadFile { get; set; }
        public string Page { get; set; }
        public string DateOgrn { get; set; }
        public string Kpp { get; set; }
        public string DateRemove { get; set; }
        private string _file = string.Empty;
        public string File
        {
            get => _file;
            set => Set(ref _file, value);
        }
    }
}