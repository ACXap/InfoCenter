using Common.Data;
using Common.Service;
using Ifns.Data;
using Ifns.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace Ifns.Service
{
    public class FoundIfnsService : IFoundIfnsService
    {
        public FoundIfnsService(IRepositoryIfns rep)
        {
            _repository = rep;
            CollectionIfns = new ObservableCollection<EntityIfns>();
            _createFile = new CreateFileOffice("Ifns");
        }

        #region PrivateField
        private readonly IRepositoryIfns _repository;
        private readonly ICreateFileOfResult _createFile;

        private readonly string _charSeparator = ";";

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
        private List<string> GetIfnsToString(string charSeparator = ";")
        {
            List<string> result = new List<string>();

            foreach (var ifns in CollectionIfns)
            {
                var form = ifns.Form ?? new Form();
                var ifnsDetails = ifns.IfnsDetails ?? new IfnsDetails();
                var pay = ifns.PayeeDetails ?? new PayeeDetails();
                var sprof = ifns.SprofDetails ?? new SproDetails();
                var sprou = ifns.SprouDetails ?? new SproDetails();

                result.Add($"{form.Ifns}{charSeparator}" +
                    $"{RemoveCharSeparator(form.Oktmmf)}{charSeparator}" +
                    $"{RemoveCharSeparator(ifnsDetails.IfnsName)}{charSeparator}" +
                    $"{RemoveCharSeparator(ifnsDetails.IfnsAddr)}{charSeparator}" +
                    $"{RemoveCharSeparator(ifnsDetails.IfnsPhone)}{charSeparator}" +
                    $"{RemoveCharSeparator(ifnsDetails.IfnsComment)}{charSeparator}" +
                    $"{RemoveCharSeparator(pay.PayeeName)}{charSeparator}" +
                    $"{RemoveCharSeparator(pay.PayeeInn)}{charSeparator}" +
                    $"{RemoveCharSeparator(pay.PayeeKpp)}{charSeparator}" +
                    $"{RemoveCharSeparator(pay.BankName)}{charSeparator}" +
                    $"{RemoveCharSeparator(pay.BankBic)}{charSeparator}" +
                    $"{RemoveCharSeparator(pay.CorrespAcc)}{charSeparator}" +
                    $"{RemoveCharSeparator(pay.PayeeAcc)}{charSeparator}" +
                    $"{RemoveCharSeparator(sprof.SproCode)}{charSeparator}" +
                    $"{RemoveCharSeparator(sprof.SproName)}{charSeparator}" +
                    $"{RemoveCharSeparator(sprof.SproAddr)}{charSeparator}" +
                    $"{RemoveCharSeparator(sprof.SproPhone)}{charSeparator}" +
                    $"{RemoveCharSeparator(sprof.SproComment)}{charSeparator}" +
                    $"{RemoveCharSeparator(sprou.SproCode)}{charSeparator}" +
                    $"{RemoveCharSeparator(sprou.SproName)}{charSeparator}" +
                    $"{RemoveCharSeparator(sprou.SproAddr)}{charSeparator}" +
                    $"{RemoveCharSeparator(sprou.SproPhone)}{charSeparator}" +
                    $"{RemoveCharSeparator(sprou.SproComment)}");
            }

            return result;
        }
        private string RemoveCharSeparator(string data)
        {
            if (string.IsNullOrEmpty(data)) return "";   
            return data.Replace(_charSeparator, " ");
        }

        #endregion PrivateMethod

        #region PublicMethod

        public Result<bool> SaveFile()
        {
            Result<bool> result = new Result<bool>();
            try
            {
                var ifnsString = GetIfnsToString(_charSeparator);

                List<string> data = new List<string>(ifnsString.Count + 1)
                {
                    GetHeader(_charSeparator)
                };

                data.AddRange(ifnsString);

                var filename = $"ifns_{DateTime.Now::dd_MM_yyyy_HH_mm}";
                var file = _createFile.CreateXlsx(data, filename);
                _createFile.CreateCsv(data, filename);

                _createFile.OpenFolderFile(file);
            }
            catch (Exception ex)
            {
                result.ErrorResult = new ErrorResult(ex.Message, EnumTypeError.ErrorFileFormat);
            }

            return result;
        }

        public async Task<Result<bool>> GetAll()
        {
            Result<bool> result = new Result<bool>();
            return await Task.Run(() =>
            {
                try
                {
                    var region = _repository.GetRegions();

                    ParallelOptions po = new ParallelOptions()
                    {
                        MaxDegreeOfParallelism = 5
                    };

                    Parallel.ForEach(region, po, (reg) =>
                    {
                        foreach (var insp in reg.Inspections)
                        {
                            insp.Municipalities = _repository.GetMunicipalities(insp);

                            Parallel.ForEach(insp.Municipalities, po, (mun) =>
                            {
                                mun.Ifns = _repository.GetEntityIfns(mun, insp);
                                Application.Current?.Dispatcher?.BeginInvoke(new Action(() =>
                                {
                                    CollectionIfns.Add(mun.Ifns);
                                }));
                            });
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