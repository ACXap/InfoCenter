using Fssp.Data;
using Fssp.Repository.Data;
using System.Collections.Generic;
using System.Linq;

namespace Fssp.Service
{
    public static class ServiceConvert
    {
        public static EntityPerson ConvertFoundPersonToEntityPerson(FoundPerson person)
        {
            var fio = ServiceFio.GetFio(person?.Fio);
            return new EntityPerson()
            {
                Lastname = fio[0],
                Firstname = fio[1],
                Secondname = fio[2],
                Birthdate = person.Date,
                Region = person.Region.Id
            };
        }

        public static EntityCompany ConvertFoundCompanyToEntityCompany(FoundCompany company)
        {
            return new EntityCompany()
            {
                Address = company?.Address,
                Name = company.Name,
                Region = company.Region.Id
            };
        }

        public static List<string> ConvertEntityResultsToStrings(IEnumerable<EntityResult> data)
        {
            var list = new List<string>()
            {
                EntityResult.GetStringPropery()
            };

            list.AddRange(data.Select(x =>
            {
                return x.ToString();
            }));

            return list;
        }

        public static List<EntityNumber> ConvertStringToEntityNumber(IEnumerable<string> data)
        {
            var list = new List<EntityNumber>();

            foreach (var item in data)
            {
                var str = item.Split(';');

                list.Add(new EntityNumber()
                {
                    Id = str[0],
                    Number = str[1]
                });
            }

            return list;
        }

        public static List<EntityPerson> ConvertStringToEntityPerson(IEnumerable<string> data)
        {
            var list = new List<EntityPerson>();

            foreach (var item in data)
            {
                var str = item.Split(';');
                var fio = ServiceFio.GetFio(str[1]);

                list.Add(new EntityPerson()
                {
                    Id = str[0],
                    Lastname = fio[0],
                    Firstname = fio[1],
                    Secondname = fio[2],
                    Birthdate = str[2],
                    Region = int.Parse(str[3])
                });
            }

            return list;
        }

        public static List<EntityCompany> ConvertStringToEntityCompany(IEnumerable<string> data)
        {
            var list = new List<EntityCompany>();

            foreach (var item in data)
            {
                var str = item.Split(';');

                list.Add(new EntityCompany()
                {
                    Id = str[0],
                    Name = str[1],
                    Address = str[2],
                    Region = int.Parse(str[3])
                });
            }

            return list;
        }

        public static IEnumerable<string> ConvertEntityResultsToStrings(EntityResponsResult result)
        {
            var list = new List<string>()
            {
                $"id;{EntityResult.GetStringPropery()}"
            };

            foreach (var item in result?.CollectionQuery)
            {
                if (item.CollectionResult != null && item.CollectionResult.Any())
                {
                    list.AddRange(item.CollectionResult.Select(x =>
                    {
                        return $"{item.Id};{x}";
                    }));
                }
                else
                {
                    list.Add($"{item.Id};Ничего не найдено");
                }
            }



            return list;
        }
    }
}