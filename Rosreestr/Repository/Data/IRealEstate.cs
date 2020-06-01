namespace Rosreestr.Repository.Data
{
    public interface IRealEstate
    {
        //Назначение объекта/назначение здания    важно

        /// <summary>
        /// Кадастровый номер
        /// </summary>
        string KadastrNumber { get; }
        /// <summary>
        /// Адрес расположения объекта
        /// </summary>
        string Address { get; }
        /// /// <summary>
        /// Площадь/протяженность
        /// </summary>
        string Area { get; }
        /// <summary>
        /// Кадастровая стоимость
        /// </summary>
        string Cost { get; }
        /// <summary>
        /// Дата внесения сведений о кадастровой стоимости
        /// </summary>
        string CostDateEntering { get; }
        /// <summary>
        /// Дата определения кадастровой стоимости
        /// </summary>
        string CostDateValuation { get; }
        /// <summary>
        /// Дата утверждения кадастровой стоимости
        /// </summary>
        string CostDateApproval { get; }

        /// <summary>
        /// Тип
        /// </summary>
        string ObjectDesc { get; }
        /// <summary>
        /// Наименование объекта/вид объекта недвижимости
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Условный номер
        /// </summary>
        string ConditionalNumber { get; }
       
        /// <summary>
        /// Ранее присвоенный номер
        /// </summary>
        string PreviouslyAssignedNumber { get; }
        /// <summary>
        /// Год ввода в эксплуатацию
        /// </summary>
        string YearUsed { get; }
        /// <summary>
        /// Год завершения строительства
        /// </summary>
        string YearBuilt { get; }
    }
}