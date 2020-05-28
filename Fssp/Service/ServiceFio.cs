using System;

namespace Fssp.Service
{
    public static class ServiceFio
    {
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

        public static string[] GetFio(string fio)
        {
            if (string.IsNullOrEmpty(fio)) return new string[2];

            var s = fio.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return new string[] { s[0], s[1], s.Length == 3 ? s[2] : "" };
        }
    }
}