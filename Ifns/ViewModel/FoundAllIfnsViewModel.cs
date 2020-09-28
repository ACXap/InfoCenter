using Common;
using GalaSoft.MvvmLight.CommandWpf;
using Ifns.Data;
using Ifns.Service;
using System.Collections.ObjectModel;

namespace Ifns.ViewModel
{
    public class FoundAllIfnsViewModel : FoundViewModelBase
    {
        public FoundAllIfnsViewModel(IFoundIfnsService foundService)
        {
            _foundService = foundService;
            CollectionIfns = new ReadOnlyObservableCollection<EntityIfns>(_foundService.CollectionIfns);
        }

        #region PrivateField
        private readonly IFoundIfnsService _foundService;

        private RelayCommand _commandStart;
        private RelayCommand _commandSave;
        #endregion PrivateField

        #region PublicProperties

        public ReadOnlyObservableCollection<EntityIfns> CollectionIfns { get; }
        #endregion PublicProperties

        #region Command
        public RelayCommand CommandStart =>
        _commandStart ?? (_commandStart = new RelayCommand(
           async () =>
           {
               StartProcess();

               var result = await _foundService.GetAll().ConfigureAwait(true);

               StopProcess(result.ErrorResult);
           }, ()=> !IsShowProgressBarFound));

        public RelayCommand CommandSave =>
        _commandSave ?? (_commandSave = new RelayCommand(
            () =>
            {
                var result = _foundService.SaveFile();
            }, ()=> CollectionIfns.Count!=0));
        #endregion Command
    }
}