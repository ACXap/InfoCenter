using System.Collections;
using System.Collections.Generic;

namespace Fssp.Repository.Data
{
    public class EntityResult
    {
        /// <summary>
        /// Должник
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Исполнительное производство
        /// </summary>
        public string Production { get; set; }
        /// <summary>
        /// Реквизиты исполнительного документа
        /// </summary>
        public string Details { get; set; }
        /// <summary>
        /// Предмет исполнения, сумма непогашенной задолженности
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Отдел судебных приставов
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// Судебный пристав-исполнитель
        /// </summary>
        public string Bailiff { get; set; }
        /// <summary>
        /// Дата, причина окончания или прекращения ИП
        /// </summary>
        public string End { get; set; }

        /// <summary>
        /// Значение свойств одной строкой
        /// </summary>
        /// <returns>Строка значений свойств</returns>
        public override string ToString()
        {
            return $"{Name};{Production};{Details};{Subject};{Department};{Bailiff};{End}";
        }

        /// <summary>
        /// Получить описание свойств одной строкой
        /// </summary>
        /// <returns>Строка описаний свойств класса</returns>
        public static string GetStringPropery()
        {
            return "Должник;Исполнительное производство;Реквизиты исполнительного документа;Предмет исполнения, сумма непогашенной задолженности;" +
                "Отдел судебных приставов;Судебный пристав-исполнитель;Дата, причина окончания или прекращения ИП";
        }
    }
}