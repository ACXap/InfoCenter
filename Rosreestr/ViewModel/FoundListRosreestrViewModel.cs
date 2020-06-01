﻿using Common;
using Common.Settings.Service;
using GalaSoft.MvvmLight.CommandWpf;
using Rosreestr.Data;
using Rosreestr.Repository.Data;
using Rosreestr.Service;
using Rosreestr.Service.Interface;
using System.Collections.ObjectModel;
using System.Linq;

namespace Rosreestr.ViewModel
{
    public class FoundListRosreestrViewModel: FoundViewModelBase
    {
        public FoundListRosreestrViewModel(IFoundRosreestrService foundService, ISettingsService settings)
        {
            _foundService = foundService;

            FoundHeader = new Common.Data.FoundHeader()
            {
                CommandFound = new RelayCommand(() => OpenFile()),
                FoundFast = false,
                Header = "Обработка списков",
                Watermark = "Выбрать файл с данными для обработки"
            };
        }

        #region PrivateField
        private readonly IFoundRosreestrService _foundService;

        private ReadOnlyCollection<EntityRealEstate> _collectionRealEstate;

        private TypeData _typeData;

        // private RelayCommand<EntityFoundRealEstate> _commandGetFile;
        private RelayCommand _commandStart;
        #endregion PrivateField

        #region PublicProperties
        public TypeData TypeData
        {
            get => _typeData;
            set => Set(ref _typeData, value);
        }
        #endregion PublicProperties

        #region Command
        public RelayCommand CommandStart =>
       _commandStart ?? (_commandStart = new RelayCommand(
                   async () =>
                   {
                       var file = FoundHeader.FoundText;
                       FoundHeader.FoundText = "";
                       StartProcess();

                       var result = await _foundService.ProcessList().ConfigureAwait(true);

                       StopProcess();
                   }, () => !string.IsNullOrEmpty(FoundHeader.FoundText) && TypeData != null && TypeData.Code == 1));
        #endregion Command

        #region PrivateMethod
        private async void OpenFile()
        {
            var file = ServiceFile.GetFile();

            if (string.IsNullOrEmpty(file) == false)
            {
                FoundHeader.FoundText = file;
                TypeData = await ServiceFile.GetTypeData(file).ConfigureAwait(false);

                var resultCol = await _foundService.GetList(file).ConfigureAwait(true);

                if (resultCol.ErrorResult == null)
                {
                    CollectionRealEstate = new ReadOnlyCollection<EntityRealEstate>(resultCol.Items.ToList());
                }
            }
        }
        #endregion PrivateMethod

        #region PublicMethod
        public ReadOnlyCollection<EntityRealEstate> CollectionRealEstate
        {
            get => _collectionRealEstate;
            set => Set(ref _collectionRealEstate, value);
        }
        #endregion PublicMethod
    }
}