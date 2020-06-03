using Egrul.Repository.Data;
using System;
using System.Collections.Generic;

namespace Egrul.Repository
{
    public class RepositoryEgrulTest : IRepositoryEgrul
    {
        public List<EntityCompany> GetCollectionCompany(string query)
        {
            return new List<EntityCompany>()
            {
                new EntityCompany()
                {
                    Address = "603116 НИЖЕГОРОДСКАЯ ОБЛАСТЬ ГОРОД НИЖНИЙ НОВГОРОД УЛИЦА ТОНКИНСКАЯ 12 1",
                    Title = "ТСЖ-345",
                    //CountRecords = "105",
                    Director = "ПРЕДСЕДАТЕЛЬ: Железнова Галина Семеновна",
                    Inn = "5257081603",
                    FullTitle = "ТОВАРИЩЕСТВО СОБСТВЕННИКОВ ЖИЛЬЯ №  345",
                    Ogrn = "1065200047330",
                    TokenLoadFile = "955EE8C127BCFCCB969C2A88348E37C636CE5B86B4AFD3950FE9729B30468C98F34BA8BA0DC391D38DC3DF6180D2B0A3EE3A184C60265485380845CB9831B7A8",
                    //Page = "1",
                    DateOgrn = "30.05.2006",
                    Kpp ="525701001"
                },
                new EntityCompany()
                {
                    Address = "454021 ЧЕЛЯБИНСКАЯ ОБЛАСТЬ ГОРОД ЧЕЛЯБИНСК УЛИЦА МОЛДАВСКАЯ 25 Б ",
                    Title = "МУП   \"АПТЕКА  № 345\"",
                    //CountRecords = "105",
                    Director = "Директор: Шишкова Зоя Павловна",
                    Inn = "7448006960",
                    FullTitle = "МУНИЦИПАЛЬНОЕ УНИТАРНОЕ ПРЕДПРИЯТИЕ \"АПТЕКА  № 345\"",
                    Ogrn = "1027402545532",
                    TokenLoadFile = "575DAE4A6C22FE10A4243EC12B23B595E82DCEACF90C740E6665886E399BD8C01B96F08B639EB9A46C2EAEFF501BCA66AF86373D122CB72828119729FB8DA8EC",
                    DateOgrn = "31.10.2002",
                    Kpp = "744801001",
                    DateRemove = "13.11.2006"
                }
            };
        }

        public bool GetFile(string token, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}