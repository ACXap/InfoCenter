using Common;
using Common.Settings.Service;
using Egrul.Data.Model;
using Egrul.Service.Interfaces;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Linq;

namespace Egrul.ViewModel
{
    public class FoundCompanyEgrulViewModel : FoundViewModelBase
    {
        public FoundCompanyEgrulViewModel(IFoundCompanyEgrulService foundEgrul, ISettingsService settings)
        {
            Header = "Предоставление сведений из ЕГРЮЛ/ЕГРИП";
            FoundHeader = new Common.Data.FoundHeader()
            {
                CommandFound = new RelayCommand(() => FoundCompany(), () => !string.IsNullOrEmpty(FoundHeader.FoundText)),
                FoundFast = false,
                Header = "Поисковый запрос",
                Watermark = "Укажите ИНН или ОГРН (ОГРНИП) или наименование ЮЛ, ФИО ИП"
            };

            TypeGrid = settings.GetSettings().TypeGrid;

            _foundService = foundEgrul;
        }

        #region PrivateField
        private readonly IFoundCompanyEgrulService _foundService;

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
                        IsShowProgressBarFound = true;
                        ErrorStatus = null;

                        var result = await _foundService.GetPdfFile(company);
                        ErrorStatus = result.ErrorResult;

                        IsShowProgressBarFound = false;
                    }));

        #endregion Command

        #region PrivateMethod
        private async void FoundCompany()
        {
            IsShowProgressBarFound = true;
            var result = await _foundService.GetCollectionCompany(FoundHeader.FoundText);

            CollectionCompanyInfo = result.Objects != null ? new ReadOnlyCollection<CompanyInfo>(result.Objects.ToList()) : null;
            ErrorStatus = result.ErrorResult;

            IsShowProgressBarFound = false;
        }
        #endregion PrivateMethod
    }
}