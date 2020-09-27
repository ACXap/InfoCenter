using Common.Data;

namespace Ifns.Data
{
    public class TypeDataIfns :TypeData
    {
        public override void Init(string str)
        {
            switch (str.ToLower())
            {
                case "ифнс":
                    Title = "Поиск по ИФНС";
                    break;
                case "мун":
                    Title = "Поиск по мун. образованию";
                    break;
                default:
                    Title = "Неверный тип данных";
                    Code = 0;
                    break;
            }
        }
    }
}