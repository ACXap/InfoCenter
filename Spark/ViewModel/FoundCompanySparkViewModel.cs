using Common;
using Common.Settings.Service;
using GalaSoft.MvvmLight.CommandWpf;
using Spark.Data.Model;
using Spark.Service;
using System.Collections.ObjectModel;
using System.Linq;

namespace Spark.ViewModel
{
    public class FoundCompanySparkViewModel : FoundViewModelBase
    {
        public FoundCompanySparkViewModel(IFoundCompanySparkService foundSpark, ISettingsService settings)
        {
            Header = "Поиск в базе данных СПАРК";
            FoundHeader = new Common.Data.FoundHeader()
            {
                CommandFound = new RelayCommand(() => Found(), () => !string.IsNullOrEmpty(FoundHeader.FoundText)),
                FoundFast = true,
                Header = "Поиск компаний",
                Watermark = "СПАРК ID, ИНН, ОГРН, ФИО"
            };

            TypeGrid = settings?.GetSettings().TypeGrid;

            _foundService = foundSpark;
        }

        #region PrivateField
        private readonly IFoundCompanySparkService _foundService;

        private ReadOnlyCollection<CompanyInfo> _collectionCompanyInfo;

        private  RelayCommand<CompanyInfo> _commandLoadPdf;
        #endregion PrivateField

        #region PublicProperties
        public ReadOnlyCollection<CompanyInfo> CollectionCompanyInfo
        {
            get => _collectionCompanyInfo;
            private set => Set(ref _collectionCompanyInfo, value);
        }
        #endregion PublicProperties

        #region Command
        public RelayCommand<CompanyInfo> CommandLoadPdf =>
        _commandLoadPdf ?? (_commandLoadPdf = new RelayCommand<CompanyInfo>(
            async (company) =>
            {
                company.IsLoadPdfFile = true;
                StartProcess();

                var result = await _foundService.GetFileCompany(company).ConfigureAwait(false);
                            
                company.IsLoadPdfFile = false;
                StopProcess(result.ErrorResult);
            }, (company)=> company!=null && !company.IsLoadPdfFile));

        #endregion Command

        #region PrivateMethod

        private async void Found()
        {
            StartProcess();

            var result = await _foundService.FoundCompany(FoundHeader.FoundText).ConfigureAwait(true);

            CollectionCompanyInfo = result.Items != null ? new ReadOnlyCollection<CompanyInfo>(result.Items.ToList()) : null;

            StopProcess(result.ErrorResult);
        }
        #endregion PrivateMethod
    }
}