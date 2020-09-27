using Common.Service;
using Ifns.Data;
using Ifns.Service;
using System;
using System.Collections.Generic;
namespace Ifns.Repository
{
    public class RepositoryIfnsSite : IRepositoryIfns
    {
        #region PrivateField
        private readonly HttpService _httpService = new HttpService("https://service.nalog.ru");
        private readonly IfnsParser _parser = new IfnsParser();

        private readonly string _urlQueryGetAllRegion = "static/tree2.html?inp=ifns&tree=SOUN_ADDRNO_UL&treeKind=LINKED&aver=3.39.11&sver=4.37.34&pageStyle=GM2";
        private readonly string _urlQueryGetIfns = "addrno-proc.json";
        #endregion PrivateField

        #region PrivateMethod
        private string GetRequestBodyMun(Inspection insp)
        {
            return $"c=getOktmmf&ifns={insp.Id}&okatom=";
        }

        private string GetRequestBodyIfns(Municipality mun, Inspection insp)
        {
            return $"c=next&step=1&npKind=ul&objectAddr=&objectAddr_zip=&objectAddr_ifns=&objectAddr_okatom=&ifns={insp.Id}&oktmmf={mun.Id}&PreventChromeAutocomplete=";
        }
        #endregion PrivateMethod

        #region PublicMethod
        public List<Region> GetRegions()
        {
            List<Region> result;

            var regionString = _httpService.RequestGet(_urlQueryGetAllRegion, HttpService.EnumContentType.Html);
            result = _parser.ParsRegion(regionString);

            return result;
        }

        public List<Municipality> GetMunicipalities(Inspection insp)
        {
            List<Municipality> result = new List<Municipality>();

            try
            {
                if (insp == null || string.IsNullOrEmpty(insp.Id)) throw new ArgumentNullException("Один из аргументов поиска пуст");

                var munString = _httpService.RequestPost(_urlQueryGetIfns, GetRequestBodyMun(insp), HttpService.EnumContentType.Post);
                result = _parser.ParsMunicipalities(munString);
            }
            catch (Exception ex)
            {
                result.Add(new Municipality() { Name = ex.Message });
            }

            return result;
        }

        public EntityIfns GetEntityIfns(Municipality mun, Inspection insp)
        {
            EntityIfns result;
            try
            {
                if (mun == null || insp == null || string.IsNullOrEmpty(mun.Id) || string.IsNullOrEmpty(insp.Id)) throw new ArgumentNullException("Один из аргументов поиска пуст");

                var ifnsString = _httpService.RequestPost(_urlQueryGetIfns, GetRequestBodyIfns(mun, insp), HttpService.EnumContentType.Post);
                result = _parser.ParsEntityIfns(ifnsString);
            }
            catch (Exception ex)
            {
                result = new EntityIfns()
                {
                    Form = new Form() { Ifns = insp?.Id, Oktmmf = mun.Id },
                    IfnsDetails = new IfnsDetails() { IfnsComment = ex.Message }
                };
            }

            return result;
        }
        #endregion PublicMethod
    }
}