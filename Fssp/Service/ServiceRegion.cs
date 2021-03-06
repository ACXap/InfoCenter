﻿using Fssp.Data;
using System.Collections.Generic;

namespace Fssp.Service
{
    /// <summary>
    /// Работа с регионами
    /// </summary>
    public class ServiceRegion : IServiceRegion
    {
        public List<Region> GetRegion()
        {
            return new List<Region>()
            {
                new Region(){ Name = "Алтайский край", Id = 22 },
                new Region(){ Name = "Амурская область", Id = 28 },
                new Region(){ Name = "Архангельская область и Ненецкий автономный округ", Id = 29 },
                new Region(){ Name = "Астраханская область", Id = 30 },
                new Region(){ Name = "Белгородская область", Id = 31 },
                new Region(){ Name = "Брянская область", Id = 32 },
                new Region(){ Name = "Владимирская область", Id = 33 },
                new Region(){ Name = "Волгоградская область", Id = 34 },
                new Region(){ Name = "Вологодская область", Id = 35 },
                new Region(){ Name = "Воронежская область", Id = 36 },
                new Region(){ Name = "Забайкальский край", Id = 75 },
                new Region(){ Name = "Ивановская область", Id = 37 },
                new Region(){ Name = "Иркутская область", Id = 38 },
                new Region(){ Name = "Кабардино-Балкария", Id = 7 },
                new Region(){ Name = "Калининградская область", Id = 39 },
                new Region(){ Name = "Калужская область", Id = 40 },
                new Region(){ Name = "Камчатский край и Чукотский автономный округ", Id = 41 },
                new Region(){ Name = "Карачаево-Черкесия", Id = 9 },
                new Region(){ Name = "Кемеровская область - Кузбасс", Id = 42 },
                new Region(){ Name = "Кировская область", Id = 43 },
                new Region(){ Name = "Костромская область", Id = 44 },
                new Region(){ Name = "Краснодарский край", Id = 23 },
                new Region(){ Name = "Красноярский край", Id = 24 },
                new Region(){ Name = "Курганская область", Id = 45 },
                new Region(){ Name = "Курская область", Id = 46 },
                new Region(){ Name = "Ленинградская область", Id = 47 },
                new Region(){ Name = "Липецкая область", Id = 48 },
                new Region(){ Name = "Магаданская область", Id = 49 },
                new Region(){ Name = "Москва", Id = 77 },
                new Region(){ Name = "Московская область", Id = 50 },
                new Region(){ Name = "Мурманская область", Id = 51 },
                new Region(){ Name = "Нижегородская область", Id = 52 },
                new Region(){ Name = "Новгородская область", Id = 53 },
                new Region(){ Name = "Новосибирская область", Id = 54 },
                new Region(){ Name = "Омская область", Id = 55 },
                new Region(){ Name = "Оренбургская область", Id = 56 },
                new Region(){ Name = "Орловская область", Id = 57 },
                new Region(){ Name = "Пензенская область", Id = 58 },
                new Region(){ Name = "Пермский край", Id = 59 },
                new Region(){ Name = "Приморский край", Id = 25 },
                new Region(){ Name = "Псковская область", Id = 60 },
                new Region(){ Name = "Республика Адыгея", Id = 1 },
                new Region(){ Name = "Республика Алтай", Id = 4 },
                new Region(){ Name = "Республика Башкортостан", Id = 2 },
                new Region(){ Name = "Республика Бурятия", Id = 3 },
                new Region(){ Name = "Республика Дагестан", Id = 5 },
                new Region(){ Name = "Республика Ингушетия", Id = 6 },
                new Region(){ Name = "Республика Калмыкия", Id = 8 },
                new Region(){ Name = "Республика Карелия", Id = 10 },
                new Region(){ Name = "Республика Коми", Id = 11 },
                new Region(){ Name = "Республика Крым", Id = 82 },
                new Region(){ Name = "Республика Марий Эл", Id = 12 },
                new Region(){ Name = "Республика Мордовия", Id = 13 },
                new Region(){ Name = "Республика Саха (Якутия)", Id = 14 },
                new Region(){ Name = "Республика Татарстан", Id = 16 },
                new Region(){ Name = "Республика Тыва", Id = 17 },
                new Region(){ Name = "Республика Хакасия", Id = 19 },
                new Region(){ Name = "Ростовская область", Id = 61 },
                new Region(){ Name = "Рязанская область", Id = 62 },
                new Region(){ Name = "Самарская область", Id = 63 },
                new Region(){ Name = "Санкт-Петербург", Id = 78 },
                new Region(){ Name = "Саратовская область", Id = 64 },
                new Region(){ Name = "Сахалинская область", Id = 65 },
                new Region(){ Name = "Свердловская область", Id = 66 },
                new Region(){ Name = "Севастополь", Id = 92 },
                new Region(){ Name = "Северная Осетия-Алания", Id = 15 },
                new Region(){ Name = "Смоленская область", Id = 67 },
                new Region(){ Name = "Ставропольский край", Id = 26 },
                new Region(){ Name = "Тамбовская область", Id = 68 },
                new Region(){ Name = "Тверская область", Id = 69 },
                new Region(){ Name = "Томская область", Id = 70 },
                new Region(){ Name = "Тульская область", Id = 71 },
                new Region(){ Name = "Тюменская область", Id = 72 },
                new Region(){ Name = "Удмуртская Республика", Id = 18 },
                new Region(){ Name = "Ульяновская область", Id = 73 },
                new Region(){ Name = "Управление по исполнению особо важных исполнительных производств", Id = 99 },
                new Region(){ Name = "Хабаровский край и Еврейская автономная область", Id = 27 },
                new Region(){ Name = "Ханты-Мансийский АО", Id = 86 },
                new Region(){ Name = "Челябинская область", Id = 74 },
                new Region(){ Name = "Чеченская Республика", Id = 20 },
                new Region(){ Name = "Чувашская Республика - Чувашия", Id = 21 },
                new Region(){ Name = "Ямало-Ненецкий АО", Id = 89 },
                new Region(){ Name = "Ярославская область", Id = 76 }
            };
        }
    }
}