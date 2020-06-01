using Common;
using Common.Settings.Service;
using GalaSoft.MvvmLight.CommandWpf;
using Rosreestr.Repository.Data;
using Rosreestr.Service.Interface;
using System.Collections.ObjectModel;
using System.Linq;

namespace Rosreestr.ViewModel
{
    public class FoundNumberRosreestrViewModel : FoundViewModelBase
    {
        public FoundNumberRosreestrViewModel(IFoundRosreestrService foundService, ISettingsService settings)
        {
            TypeGrid = settings?.GetSettings().TypeGrid;

            _foundService = foundService;

            FoundHeader = new Common.Data.FoundHeader()
            {
                CommandFound = new RelayCommand(() => Found(), () => !string.IsNullOrEmpty(FoundHeader.FoundText)),
                FoundFast = false,
                Header = "Поиск объектов недвижимости",
                Watermark = "Кадастровый номер"
            };
        }

        #region PrivateField
        private readonly IFoundRosreestrService _foundService;

        private ReadOnlyCollection<EntityFoundRealEstate> _collectionFoundRealEstate;

        private RelayCommand<EntityFoundRealEstate> _commandGetFile;
        #endregion PrivateField

        #region PublicProperties
        public ReadOnlyCollection<EntityFoundRealEstate> CollectionFoundRealEstate
        {
            get => _collectionFoundRealEstate;
            private set => Set(ref _collectionFoundRealEstate, value);
        }

        #endregion PublicProperties

        #region Command
        public RelayCommand<EntityFoundRealEstate> CommandGetFile =>
        _commandGetFile ?? (_commandGetFile = new RelayCommand<EntityFoundRealEstate>(
            async req =>
            {
                StartProcess();
                var result = await _foundService.GetRealEstate(req);

                StopProcess(result.ErrorResult);
            }));
        #endregion Command

        #region PrivateMethod
        private async void Found()
        {
            StartProcess();

            var result = await _foundService.FoundRealEstate(FoundHeader.FoundText).ConfigureAwait(true);

            CollectionFoundRealEstate = result.Items != null ? new ReadOnlyCollection<EntityFoundRealEstate>(result.Items.ToList()) : null;

            StopProcess(result.ErrorResult);
        }
        #endregion PrivateMethod
    }
}