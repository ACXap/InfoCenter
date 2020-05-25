using Fssp.Data;
using Fssp.Repository.Data;
using System.Collections.Generic;

namespace Fssp.Repository
{
    public interface IRepositoryFssp
    {
        /// <summary>
        /// Отправить запрос на поиск физического лица
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        EntityResultSearch SearchPerson(EntityPerson person, string key);

        /// <summary>
        /// Отправить запрос на поиск юридического лица
        /// </summary>
        /// <returns></returns>
        string SearchCompany();

        /// <summary>
        /// Отправить запрос на поиск по номеру исполнительного производства
        /// </summary>
        /// <returns></returns>
        string SearchIp();

        /// <summary>
        /// Отправить групповой запрос
        /// </summary>
        /// <returns></returns>
        string SearchGroop();

        /// <summary>
        /// Отправить запрос на получение статуса выполнения задачи
        /// </summary>
        /// <param name="token"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        EntityStatus Status(string token, string key);

        /// <summary>
        /// Отправить запрос на получение результата выполнения задачи
        /// </summary>
        /// <param name="token"></param>
        /// <param name="key"></param>
        EntityResponsResult Result(string token, string key);
    }
}