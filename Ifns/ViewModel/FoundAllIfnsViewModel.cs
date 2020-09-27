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
    public class FoundAllIfnsViewModel : FoundViewModelBase
    {
        public FoundAllIfnsViewModel(IFoundIfnsService foundService)
        {
            _foundService = foundService;

            FoundHeader = new Common.Data.FoundHeader()
            {
                CommandFound = new RelayCommand(() => { }),
                FoundFast = false,
                Header = "Поиск и сохранения всех ИФНС"
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