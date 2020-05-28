using Common;
using Common.Settings.Service;
using Egrul.Data.Model;
using Egrul.Service;
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
                CommandFound = new RelayCommand(() => Found(), () => !string.IsNullOrEmpty(FoundHeader.FoundText)),
                FoundFast = false,
                Header = "Поисковый запрос",
                Watermark = "Укажите ИНН или ОГРН (ОГРНИП) или наименование ЮЛ, ФИО ИП"
            };

            TypeGrid = settings?.GetSettings().TypeGrid;

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
                        StartProcess();

                        var result = await _foundService.GetPdfFile(company).ConfigureAwait(false);
                       
                        StopProcess(result.ErrorResult);
                    }));

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