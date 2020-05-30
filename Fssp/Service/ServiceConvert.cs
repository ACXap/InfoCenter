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

        public static List<EntityPerson> ConvertStringToEntityPerson(IEnumerable<string> data)
        {
            var list = new List<EntityPerson>();

            foreach(var item in data)
            {
                var str = item.Split(';');
                var fio = ServiceFio.GetFio(str[0]);

                list.Add(new EntityPerson()
                {
                    Lastname = fio[0],
                    Firstname = fio[1],
                    Secondname = fio[2],
                    Birthdate = str[1],
                    Region = int.Parse(str[2])
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
                    Name = str[0],
                    Address = str[1],
                    Region = int.Parse(str[2])
                });
            }

            return list;
        }

        public static IEnumerable<string> ConvertEntityResultsToStrings(EntityResponsResult result)
        {
            var list = new List<string>()
            {
                EntityResult.GetStringPropery()
            };

            foreach(var item in result.CollectionQuery)
            {
                list.Add($"{item.Lastname} {item.Firstname} {item.Secondname} {item.Name} {item.Number} {(item.Region == 0 ? "":item.Region.ToString())};{item.Error} ");

                if (item.CollectionResult != null)
                {
                    list.AddRange(item.CollectionResult.Select(x =>
                    {
                        return x.ToString();
                    }));
                }
            }

            return list;
        }
    }
}