using System;

namespace Fssp.Service
{
    /// <summary>
    /// Работа с ФИО
    /// </summary>
    public static class ServiceFio
    {
        /// <summary>
        /// Проверка фамилии на корректность
        /// </summary>
        /// <param name="fio">ФИО строкой</param>
        public static bool CheckFio(string fio)
        {
            //если пусто
            if (string.IsNullOrEmpty(fio)) return false;
            
            //содержит что-то кроме буквы и пробела
            foreach (var c in fio)
            {
                if (!char.IsLetter(c) && c!=' ')
                {
                    return false;
                }
            }

            //содержит меньше 2 слов или больше 4
            var s = fio.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (s.Length < 2 || s.Length > 4)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Разделяем ФИО на части
        /// </summary>
        /// <param name="fio"></param>
        public static string[] GetFio(string fio)
        {
            if (string.IsNullOrEmpty(fio) || string.IsNullOrWhiteSpace(fio)) throw new Exception("ФИО не заполнена");

            var s = fio.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if(s.Length<2) throw new Exception("Неверный формат ФИО");

            return new string[] { s[0], s[1], s.Length == 3 ? s[2] : "" };
        }
    }
}