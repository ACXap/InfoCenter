﻿using Common.Data;
using Common.Service;
using Rosreestr.Repository;
using Rosreestr.Repository.Data;
using Rosreestr.Repository.Data.Json;
using Rosreestr.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rosreestr.Service
{
    public class FoundServiceRosresstr : IFoundRosreestrService
    {
        public FoundServiceRosresstr(IRepositoryRosreestr rep)
        {
            _repositoryRosreestr = rep;

            _createFile = new CreateFileOffice("Rosreestr");
        }

        #region PrivateField
        private readonly IRepositoryRosreestr _repositoryRosreestr;
        private readonly ICreateFileOfResult _createFile;

        private string _fileName;
        #endregion PrivateField

        #region PublicProperties
        #endregion PublicProperties

        #region PrivateMethod
        #endregion PrivateMethod

        #region PublicMethod
        public async Task<Result<EntityFoundRealEstate>> FoundRealEstate(string query)
        {
            return await Task.Run(() =>
            {
                Result<EntityFoundRealEstate> result = new Result<EntityFoundRealEstate>();

                try
                {
                    var list = _repositoryRosreestr.FoundRealEstate(query);

                    result.Items = list;

                    if (list == null || !result.Items.Any())
                    {
                        result.ErrorResult = new ErrorResult("Данных нет", EnumTypeError.ResultNotFound);
                    }
                }
                catch (Exception ex)
                {
                    result.ErrorResult = new ErrorResult(ex.Message, EnumTypeError.ErrorSite);
                    // Тут будет логер
                }

                return result;
            }).ConfigureAwait(false);
        }

        public async Task<Result<IRealEstate>> GetRealEstate(EntityFoundRealEstate estate)
        {
            return await Task.Run(() =>
            {
                Result<IRealEstate> result = new Result<IRealEstate>();

                try
                {
                    var item = _repositoryRosreestr.GetRealEstate(estate.NobjectCn);

                    var resultExel = _createFile.CreateXlsx(ServiceConvert.GetConvertEstateToStrings(item), estate.NobjectCn);

                    _createFile.OpenFolderFile(resultExel);

                    if (item == null)
                    {
                        result.ErrorResult = new ErrorResult("Данных нет", EnumTypeError.ResultNotFound);
                    }

                    result.Item = item;
                }
                catch (Exception ex)
                {
                    result.ErrorResult = new ErrorResult(ex.Message, EnumTypeError.ErrorSite);
                    // Тут будет логер
                }

                return result;
            }).ConfigureAwait(false);
        }
        #endregion PublicMethod

        public List<EntityRealEstate> CollectionEstate { get; private set; }

        public async Task<Result<bool>> ProcessList()
        {
            Result<bool> result = new Result<bool>();
            return await Task.Run(() =>
            {
                try
                {
                    Parallel.ForEach(CollectionEstate, item =>
                    {
                        try
                        {
                            var a = _repositoryRosreestr.GetRealEstate(item.Id);

                            if (a != null)
                            {
                                item.Estate = a;
                            }
                            else
                            {
                                item.Estate = new ErrorEstate("Ничего не найдено");
                            }
                        }
                        catch(Exception ex)
                        {
                            item.Estate = new ErrorEstate(ex.Message);
                        }
                    });


                    var file = _createFile.CreateXlsx(ServiceConvert.ConvertCollectionEntityEstate(CollectionEstate), _fileName);

                    _createFile.OpenFolderFile(file);

                    result.Item = true;
                }
                catch (Exception ex)
                {
                    result.ErrorResult = new ErrorResult(ex.Message, EnumTypeError.ErrorSite);
                }

                return result;
            });
        }

        public async Task<Result<EntityRealEstate>> GetList(string file)
        {
            Result<EntityRealEstate> result = new Result<EntityRealEstate>();
            CollectionEstate = new List<EntityRealEstate>();
            result.Items = CollectionEstate;

            return await Task.Run(() =>
            {
                try
                {
                    var str = ServiceFile.ReadFile(file).Skip(1);
                    CollectionEstate.AddRange(str.Select(x =>
                    {
                        return new EntityRealEstate()
                        {
                            Id = x
                        };
                    }));

                    _fileName = ServiceFile.GetOnlyFileName(file);
                }
                catch (Exception ex)
                {
                    result.ErrorResult = new ErrorResult(ex.Message, EnumTypeError.ErrorBd);
                }

                return result;
            });
        }
    }
}