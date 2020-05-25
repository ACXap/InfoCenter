using GalaSoft.MvvmLight;

namespace Spark.Data.Model
{
    public class CompanyInfo:ViewModelBase
    {
        public string Link { get; set; }
        public string Inn { get; set; }
        public string Ogrn { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Director { get; set; }

        private bool _isLoadPdfFile = false;
        public bool IsLoadPdfFile
        {
            get => _isLoadPdfFile;
            set => Set(ref _isLoadPdfFile, value);
        }

    }
}