using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Common.Data
{
    public class FoundHeader:ViewModelBase
    {
        public string Header { get; set; }
        public string Watermark {get;set;}
        public bool FoundFast { get; set; }

        private string _foundText = string.Empty;
        public string FoundText
        {
            get => _foundText;
            set
            {
                Set(ref _foundText, value);
                
                if (value!=null && value.Length > 2 && FoundFast)
                {
                    CommandFound.Execute(null);
                }
            }
        }
        public RelayCommand CommandFound { get; set; }
    }
}