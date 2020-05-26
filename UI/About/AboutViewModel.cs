using GalaSoft.MvvmLight;

namespace UI.About
{
    public class AboutViewModel :ViewModelBase
    {
        private string _textAbout = "Тут будет информация о приложении";
        public string TextAbout
        {
            get => _textAbout;
            private set => Set(ref _textAbout, value);
        }
    }
}