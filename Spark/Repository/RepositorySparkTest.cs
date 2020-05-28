using HtmlAgilityPack;
using Spark.Repository.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Spark.Repository
{
    public class RepositorySparkTest : IRepositorySpark
    {
        public List<EntityCompanyInfo> FoundCompany(string query)
        {
            var list = new List<EntityCompanyInfo>()
            {
                new EntityCompanyInfo(){Inn = "2901120811", Ogrn = "1042900000241", Address = "г Архангельск, Новгородский пр-кт, д 32", Director = "Ширяев Евгений Александрович",
                    Link = "/mdm/ExtendedReport?INN=2901120811_ОАО \"ИПП \"ПРАВДА СЕВЕРА\"&eliminated=false", Title = "ОАО \"ИПП \"ПРАВДА СЕВЕРА\"" },
                new EntityCompanyInfo(){Inn = "7714086422", Ogrn = "1027700451976", Address = "г Москва, Ленинградский пр-кт, д 49", Director = "Эскиндаров Мухадин Абдурахманович",
                    Link = "/mdm/ExtendedReport?INN=7714086422_ФИНАНСОВЫЙ УНИВЕРСИТЕТ, ФИНУНИВЕРСИТЕТ&eliminated=false", Title = "ФИНАНСОВЫЙ УНИВЕРСИТЕТ, ФИНУНИВЕРСИТЕТ" },
                new EntityCompanyInfo(){Inn = "7805309316", Ogrn = "1157847094822", Address = "198188, ГОРОД САНКТ-ПЕТЕРБУРГ, УЛИЦА ЗАЙЦЕВА, ДОМ 41, ЛИТЕР А, ПОМЕЩЕНИЕ 14-Н:44 ОФИС 253 ",
                    Director = "Чесноков Станислав Валерьевич", Link = "/mdm/ExtendedReport?INN=7805309316_ПАО \"ДЕВЕЛОПЕРСКИЙ ЦЕНТР \"ПРАЙМ\"&eliminated=false", Title = "ПАО \"ДЕВЕЛОПЕРСКИЙ ЦЕНТР \"ПРАЙМ\"" },
                new EntityCompanyInfo(){Inn = "5078014128", Ogrn = "1045011902484", Address = "Московская обл, г Талдом, село Квашёнки, д 19", Director = "Галицкий Дмитрий Александрович",
                    Link = "/mdm/ExtendedReport?INN=5078014128_ОАО \"СП \"ПРАВДА\"&eliminated=false", Title = "ОАО \"СП \"ПРАВДА\"" },
                new EntityCompanyInfo(){Inn = "3923000132", Ogrn = "1023902270622", Address = "Калининградская обл, г Правдинск, ул Кутузова, д 11 ", Director = "Крапивная Галина Николаевна",
                    Link = "/mdm/ExtendedReport?INN=3923000132_ОАО \"ПРАВДИНСКИЙ СЫРОДЕЛЬНЫЙ ЗАВОД\"&eliminated=false", Title = "ОАО \"ПРАВДИНСКИЙ СЫРОДЕЛЬНЫЙ ЗАВОД\"" }
            };

            return list;
        }

        public EntityCompany GetCompany(string quert)
        {
            var str = File.ReadAllText("temp.txt");

            var doc = new HtmlDocument();

            doc.LoadHtml(str);

            RemoveControlsByClass(doc, "headerLogo");
            RemoveControlsByClass(doc, "phoneModalContainer");
            RemoveControlsByClass(doc, "modalContainer");

            var a = doc.DocumentNode.Descendants(0).Where(n => n.Id =="preloadINN").First();
            a.ParentNode.RemoveChild(a);

            a = doc.DocumentNode.Descendants(0).Where(n => n.Name == "nav").First();
            a.ParentNode.RemoveChild(a);

            var company = new EntityCompany() { Html= doc.DocumentNode.OuterHtml};

            return company;
        }

        private static void RemoveControlsByClass(HtmlDocument doc, string className)
        {
            var a = doc.DocumentNode.Descendants(0).Where(n => n.HasClass(className)).First();
            a.ParentNode.RemoveChild(a);
        }
    }
}