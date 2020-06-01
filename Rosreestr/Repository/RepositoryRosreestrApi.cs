using Common.Service;
using Newtonsoft.Json;
using Rosreestr.Repository.Data;
using Rosreestr.Repository.Data.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rosreestr.Repository
{
    public class RepositoryRosreestrApi : IRepositoryRosreestr
    {
        #region PrivateField
        private readonly HttpService _httpService = new HttpService("https://rosreestr.ru");
        #endregion PrivateField

        #region PrivateMethod
        #endregion PrivateMethod

        #region PublicMethod
        public List<EntityFoundRealEstate> FoundRealEstate(string query)
        {
            var url = $"api/online/fir_objects/{_httpService.UrlEncode(query)}";

            var str = _httpService.RequestGet(url, HttpService.EnumContentType.Json);

            //if (string.IsNullOrEmpty(str)) throw new Exception("Данные не найдены");

            var e = JsonConvert.DeserializeObject<List<JsonFoundRealEstate>>(str);

            return e?.Select(x =>
            {
                return new EntityFoundRealEstate()
                {
                    AddressNotes = x.AddressNotes,
                    Apartment = x.Apartment,
                    House = x.House,
                    NobjectCn = x.NobjectCn,
                    NobjectCon = x.NobjectCon,
                    ObjectCn = x.ObjectCn,
                    ObjectCon = x.ObjectCon,
                    ObjectId = x.ObjectId,
                    ObjectType = x.ObjectType,
                    Okato = x.Okato,
                    RegionId = x.RegionId,
                    RegionKey = x.RegionKey,
                    SettlementId = x.SettlementId,
                    SrcObject = x.SrcObject,
                    Street = x.Street,
                    SubjectId = x.SubjectId
                };
            }).ToList();
        }

        public IRealEstate GetRealEstate(string query)
        {
            var url = $"fir_lite_rest/api/gkn/fir_lite_object/{query}";

            var str = _httpService.RequestGet(url, HttpService.EnumContentType.Json);

            var obj = JsonConvert.DeserializeObject<JsonObject>(str);

            IRealEstate realEstate = null;

            switch (obj.Type)
            {
                case "flat":
                    realEstate = JsonConvert.DeserializeObject<JsonFlat>(str);
                    break;

                case "construction":
                    realEstate = JsonConvert.DeserializeObject<JsonConstruction>(str);
                    break;

                case "parcel":
                    realEstate = JsonConvert.DeserializeObject<JsonParcel>(str);
                    break;

                case "building":
                    realEstate = JsonConvert.DeserializeObject<JsonBuilding>(str);
                    break;

                default:
                    break;
            }

            return realEstate;
        }
        #endregion PublicMethod
    }
}