using Common.Service;
using HtmlAgilityPack;
using Spark.Repository.Data;
using System.Collections.Generic;
using System.Linq;

namespace Spark.Repository
{
    public class RepositorySparkSite : IRepositorySpark
    {
        #region PrivateField
        private readonly HttpService _httpService = new HttpService("https://dataport.rt.ru");
        #endregion PrivateField

        #region PublicMethod
        public List<EntityCompanyInfo> FoundCompany(string query)
        {
            var requestBody = $"query={{\"query\":\"{query}\"}}";

            var str = _httpService.RequestPost("mdm/ExtendedReportCDI", requestBody, HttpService.EnumContentType.Html);
            
            var c = GetItemsCompany(str);

            var list = new List<EntityCompanyInfo>();

            foreach (var item in c)
            {
                var a = item.Descendants("a").FirstOrDefault();

                var s = a.InnerText.Split('|');

                list.Add(new EntityCompanyInfo()
                {
                    Link = a.GetAttributeValue("href", null),
                    Address = s[3],
                    Director = s[4],
                    Inn = s[0],
                    Ogrn = s[1],
                    Title = s[2]
                }); ;
            }

            return list;
        }

        public EntityCompany GetCompany(string quert)
        {
            EntityCompany company;
            var str = _httpService.RequestGet(quert, HttpService.EnumContentType.Html);
            var doc = new HtmlDocument();

            doc.LoadHtml(str);

            RemoveControl(doc);

            company = new EntityCompany()
            {
                Html = doc.DocumentNode.OuterHtml
            };

            return company;
        }
        #endregion PublicMethod

        #region PrivateMethod 
        private static IEnumerable<HtmlNode> GetItemsCompany(string str)
        {
            var doc = new HtmlDocument();

            doc.LoadHtml(str);

            var c = doc.DocumentNode.Descendants("div").Where(x => x.HasClass("link"));
            return c;
        }
        private static void RemoveControlsByClass(HtmlDocument doc, string className)
        {
            var a = doc.DocumentNode.Descendants(0).Where(n => n.HasClass(className)).First();
            a.ParentNode.RemoveChild(a);
        }
        private static void RemoveControl(HtmlDocument doc)
        {
            RemoveControlsByClass(doc, "headerLogo");
            RemoveControlsByClass(doc, "phoneModalContainer");
            RemoveControlsByClass(doc, "modalContainer");
            
            var a = doc.DocumentNode.Descendants(0).Where(n => n.Id == "preloadINN").First();
            a.ParentNode.RemoveChild(a);

            a = doc.DocumentNode.Descendants(0).Where(n => n.Name == "nav").First();
            a.ParentNode.RemoveChild(a);
        }
        #endregion PrivateMethod
    }
}