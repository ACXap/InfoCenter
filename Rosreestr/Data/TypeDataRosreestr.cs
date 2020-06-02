using Common.Data;

namespace Rosreestr.Data
{
    public class TypeDataRosreestr : TypeData
    {
        public override void Init(string str)
        {
            switch (str.ToLower())
            {
                case "кадастровый номер":
                    Title = "Кадастровые номера";
                    break;
                case "условный номер":
                    Title = "Условные номера";
                    break;
                default:
                    Title = "Неверный тип данных";
                    Code = 0;
                    break;
            }
        }
    }
}