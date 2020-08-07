using Common.Data;

namespace Fssp.Data
{
    public class TypeDataFssp : TypeData
    {
        public TypeDataFssp() { }

        public override void Init(string str)
        {
            str = str.ToLower().TrimEnd(new char[] { ';' });
            switch (str)
            {
                case "id;фио;дата рождения;регион":
                    Title = "Физические лица";
                    break;
                case "id;название;адрес;регион":
                    Title = "Юридические лица";
                    break;
                case "id;номер":
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