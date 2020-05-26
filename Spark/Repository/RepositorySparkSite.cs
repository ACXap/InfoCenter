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
        private readonly string _urlRequestPost = "http://dataport.rt.ru/mdm/ExtendedReportCDI";
        private readonly string _urlRequestGetInn = "http://dataport.rt.ru";

        private readonly HttpService _httpService = new HttpService();
        #endregion PrivateField

        #region PublicMethod
        public List<EntityCompanyInfo> GetCollectionCompany(string query)
        {
            var list = new List<EntityCompanyInfo>();

            var requestBody = $"query={{\"query\":\"{query}\"}}";

            var str = _httpService.RequestPost(_urlRequestPost, requestBody, HttpService.EnumContentType.Html);

            var doc = new HtmlDocument();

            doc.LoadHtml(str);

            var c = doc.DocumentNode.Descendants("div").Where(x => x.HasClass("link"));

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
            var str = _httpService.RequestGet(_urlRequestGetInn, quert, HttpService.EnumContentType.Html);
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