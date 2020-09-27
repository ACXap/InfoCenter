using Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Ifns.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ifns.ViewModel
{
    public class FoundListIfnsViewModel : FoundViewModelBase
    {
        public FoundListIfnsViewModel(IFoundIfnsService foundService)
        {
            _foundService = foundService;

            FoundHeader = new Common.Data.FoundHeader()
            {
                CommandFound = new RelayCommand(() => { }),
                FoundFast = false,
                Header = "Обработка списков",
                Watermark = "Выбрать файл с данными для обработки"
            };
        }

        #region PrivateField
        private readonly IFoundIfnsService _foundService;
        #endregion PrivateField

        #region PublicProperties
        #endregion PublicProperties

        #region Command
        #endregion Command

        #region PrivateMethod
        #endregion PrivateMethod

        #region PublicMethod
        #endregion PublicMethod

    }
}