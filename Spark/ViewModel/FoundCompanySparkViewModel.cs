using Common;
using Common.Settings.Service;
using GalaSoft.MvvmLight.CommandWpf;
using Spark.Data.Model;
using Spark.Service.Interfaces;
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

        private RelayCommand<CompanyInfo> _commandLoadPdf;
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
                IsShowProgressBarFound = true;
                ErrorStatus = null;

                var result = await _foundService.GetPdfFile(company).ConfigureAwait(false);
                
                ErrorStatus = result.ErrorResult;
                company.IsLoadPdfFile = false;
                IsShowProgressBarFound = false;
            }, (company)=> !company.IsLoadPdfFile));

        #endregion Command

        #region PrivateMethod
        private async void Found()
        {
            IsShowProgressBarFound = true;
            ErrorStatus = null;

            var result = await _foundService.GetCollectionCompany(FoundHeader.FoundText).ConfigureAwait(false);

            CollectionCompanyInfo = result.Objects != null ? new ReadOnlyCollection<CompanyInfo>(result.Objects.ToList()) : null;
            ErrorStatus = result.ErrorResult;

            IsShowProgressBarFound = false;
        }
        #endregion PrivateMethod
    }
}