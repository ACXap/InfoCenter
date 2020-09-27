using Common.Data;
using Common.Service;
using Ifns.Data;
using Ifns.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Ifns.Service
{
    public class FoundIfnsService : IFoundIfnsService
    {
        public FoundIfnsService(IRepositoryIfns rep)
        {
            _repository = rep;
            CollectionIfns = new ObservableCollection<EntityIfns>();
            _createFile = new CreateFileOffice("Ifns");
            //BindingOperations.EnableCollectionSynchronization(CollectionIfns, _lock);
        }

        #region PrivateField
        private readonly IRepositoryIfns _repository;
        private readonly ICreateFileOfResult _createFile;
        private object _lock = new object();

        private string _charSeparator = ";";

        #endregion PrivateField

        #region PublicProperties
        public ObservableCollection<EntityIfns> CollectionIfns { get; private set; }
        #endregion PublicProperties

        #region PrivateMethod
        private string GetHeader(string charSeparator = ";")
        {
            return $"Код ИФНС{charSeparator}" +
                $"Муниципальное образование{charSeparator}" +
                $"Наименование{charSeparator}" +
                $"Адрес{charSeparator}" +
                $"Телефон{charSeparator}" +
                $"Доп. информация{charSeparator}" +
                $"Получатель платежа{charSeparator}" +
                $"ИНН получателя{charSeparator}" +
                $"КПП получателя{charSeparator}" +
                $"Банк получателя{charSeparator}" +
                $"БИК{charSeparator}" +
                $"Корр. счет №{charSeparator}" +
                $"Счет №{charSeparator}" +
                $"Код регистрирующего органа{charSeparator}" +
                $"Наименование{charSeparator}" +
                $"Адрес{charSeparator}" +
                $"Телефон{charSeparator}" +
                $"Доп. информация{charSeparator}" +
                $"Код регистрирующего органа{charSeparator}" +
                $"Наименование{charSeparator}" +
                $"Адрес{charSeparator}" +
                $"Телефон{charSeparator}" +
                $"Доп. информация";
        }

        public List<string> GetIfnsToString(string charSeparator = ";")
        {
            List<string> result = new List<string>();

            foreach (var ifns in CollectionIfns)
            {
                var form = ifns.Form;
                var ifnsDetails = ifns.IfnsDetails;
                var pay = ifns.PayeeDetails;
                var sprof = ifns.SprofDetails;
                var sprou = ifns.SprouDetails;

                result.Add($"{form.Ifns}{charSeparator}" +
                    $"{form.Oktmmf}{charSeparator}" +
                    $"{ifnsDetails.IfnsName}{charSeparator}" +
                    $"{ifnsDetails.IfnsAddr.Trim(new char[] { ',' })}{charSeparator}" +
                    $"{ifnsDetails.IfnsPhone}{charSeparator}" +
                    $"{ifnsDetails.IfnsComment}{charSeparator}" +
                    $"{pay.PayeeName}{charSeparator}" +
                    $"{pay.PayeeInn}{charSeparator}" +
                    $"{pay.PayeeKpp}{charSeparator}" +
                    $"{pay.BankName}{charSeparator}" +
                    $"{pay.BankBic}{charSeparator}" +
                    $"{pay.CorrespAcc}{charSeparator}" +
                    $"{pay.PayeeAcc}{charSeparator}" +
                    $"{sprof.SproCode}{charSeparator}" +
                    $"{sprof.SproName}{charSeparator}" +
                    $"{sprof.SproAddr}{charSeparator}" +
                    $"{sprof.SproPhone}{charSeparator}" +
                    $"{sprof.SproComment}{charSeparator}" +
                    $"{sprou.SproCode}{charSeparator}" +
                    $"{sprou.SproName}{charSeparator}" +
                    $"{sprou.SproAddr}{charSeparator}" +
                    $"{sprou.SproPhone}{charSeparator}" +
                    $"{sprou.SproComment}");
            }

            return result;
        }

        private void SaveFile()
        {
            var ifnsString = GetIfnsToString(_charSeparator);

            List<string> data = new List<string>(ifnsString.Count + 1)
            {
                GetHeader(_charSeparator)
            };

            data.AddRange(ifnsString);

            var filename = $"ifns_{DateTime.Now::dd_MM_yyyy_hh_mm}";
            var file = _createFile.CreateXlsx(data, filename);
            _createFile.CreateCsv(data, filename);

            _createFile.OpenFolderFile(file);
        }

        #endregion PrivateMethod

        #region PublicMethod

        public async Task<Result<bool>> GetAll()
        {
            Result<bool> result = new Result<bool>();
            return await Task.Run(() =>
            {
                try
                {
                    var region = _repository.GetRegions();

                    Parallel.ForEach(region, (reg) =>
                    {
                        foreach (var insp in reg.Inspections)
                        {
                            insp.Municipalities = _repository.GetMunicipalities(insp);

                            foreach (var mun in insp.Municipalities)
                            {
                                mun.Ifns = _repository.GetEntityIfns(mun, insp);
                                Application.Current.Dispatcher.BeginInvoke(new Action (() =>
                                {
                                    CollectionIfns.Add(mun.Ifns);
                                }));
                            }
                        }
                    });

                    SaveFile();
                }
                catch (Exception ex)
                {
                    result.ErrorResult = new ErrorResult(ex.Message, EnumTypeError.ErrorSite);
                }

                return result;
            });
        }

        #endregion PublicMethod

    }
}