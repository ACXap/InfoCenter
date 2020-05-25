using GalaSoft.MvvmLight;
using System.Collections.Generic;
using UI.Data;

namespace UI.Home.Data.DesignTime
{
    public class CardsViewModelDesignTime : ViewModelBase
    {
        public CardsViewModelDesignTime()
        {
            CollectionModule = _pluginRegister.GetPlugin();
        }
       
        #region PrivateField
        private PluginRegister _pluginRegister = new PluginRegister();
        private List<EntityPlugin> _collectionModule;
        #endregion PrivateField

        #region PublicProperties
        public List<EntityPlugin> CollectionModule
        {
            get => _collectionModule;
            private set => Set(ref _collectionModule, value);
        }
        #endregion PublicProperties
    }
}