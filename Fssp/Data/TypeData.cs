using GalaSoft.MvvmLight;

namespace Fssp.Data
{
    public class TypeData:ViewModelBase
    {
        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            private set => Set(ref _title, value);
        }

        private int _code = 1;
        public int Code
        {
            get => _code;
            private set => Set(ref _code, value);
        }

        public TypeData(string str)
        {
            var s = str.ToLower();
            switch (s)
            {
                case "фио;дата рождения;регион":
                    Title = "Физические лица";
                    break;
                case "название;адрес;регион":
                    Title = "Юридические лица";
                    break;
                case "номер":
                    Title = "Номера исполнительных производств";
                    break;
                default:
                    Title = "Неверный тип данных";
                    Code = 0;
                    break;
            }
        }
    }
}