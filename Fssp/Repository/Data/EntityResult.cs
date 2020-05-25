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
    }
}