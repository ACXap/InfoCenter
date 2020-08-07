using Rosreestr.Repository.Data;
using Rosreestr.Repository.Data.Json;
using System.Collections.Generic;
using System.Linq;

namespace Rosreestr.Service
{
    public static class ServiceConvert
    {
        public static List<string> GetConvertEstateToStrings(IRealEstate data)
        {
            var list = new List<string>
            {
                $"Кадастровый номер;{data.KadastrNumber}",
                $"Адрес расположения объекта;{data.Address}",
                $"Площадь/протяженность;{data.Area}",
                $"Кадастровая стоимость;{data.Cost}",
                $"Дата внесения сведений о кадастровой стоимости;{data.CostDateEntering}",
                $"Дата определения кадастровой стоимости;{data.CostDateValuation}",
                $"Дата утверждения кадастровой стоимости;{data.CostDateApproval}",
                $"Тип;{data.ObjectDesc}",
                $"Наименование объекта/вид объекта недвижимости;{data.Name}",
                $"Условный номер;{data.ConditionalNumber}",
                $"Ранее присвоенный номер;{data.PreviouslyAssignedNumber}",
                $"Год ввода в эксплуатацию;{data.YearUsed}",
                $"Год завершения строительства;{data.YearBuilt}"
            };

            return list;
        }

        public static string GetEstateToString(IRealEstate data)
        {
            return $"{data.KadastrNumber};" +
                $"{ReplaceBadChar(data.Address)};" +
                $"{data.Area};" +
                $"{data.Cost};" +
                $"{data.CostDateEntering};" +
                $"{data.CostDateValuation};" +
                $"{data.CostDateApproval};" +
                $"{ReplaceBadChar(data.ObjectDesc)};" +
                $"{ReplaceBadChar(data.Name)};" +
                $"{data.ConditionalNumber};" +
                $"{data.PreviouslyAssignedNumber};" +
                $"{data.YearUsed};" +
                $"{data.YearBuilt};";
        }

        private static string ReplaceBadChar(string data)
        {
            return data?.Replace(';', ' ').Replace('\n', ' ');
        }

        public static string GetNameFildEsatet()
        {
            return "Кадастровый номер;" +
                "Адрес расположения объекта;" +
                "Площадь/протяженность;" +
                "Кадастровая стоимость;" +
                "Дата внесения сведений о кадастровой стоимости;" +
                "Дата определения кадастровой стоимости;" +
                "Дата утверждения кадастровой стоимости;" +
                "Тип;" +
                "Наименование объекта/вид объекта недвижимости;" +
                "Условный номер;" +
                "Ранее присвоенный номер;" +
                "Год ввода в эксплуатацию;" +
                "Год завершения строительства;";
        }

        public static string ConvertEntityEstateToString(EntityRealEstate data)
        {
            return $"{data.Id};{GetEstateToString(data.Estate)}";
        }

        public static string ConvertNameFildEntityEstateToString()
        {
            return $"Номер;{GetNameFildEsatet()}";
        }

        public static List<string> ConvertCollectionErrorEntityEstate(IEnumerable<EntityRealEstate> data)
        {
            var errors = data.Where(x => x.Estate is ErrorEstate);
            var list = new List<string>
            {
                "Кадастровый номер"
            };

            list.AddRange(errors.Select(x =>
            {
                return x.Id;
            }));

            return list;
        }

        public static List<string> ConvertCollectionEntityEstate(IEnumerable<EntityRealEstate> data)
        {
            var list = new List<string>
            {
                ConvertNameFildEntityEstateToString()
            };

            list.AddRange(data.Select(x =>
            {
                return ConvertEntityEstateToString(x);
            }));

            return list;
        }
    }
}