using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;
using UI.Data;

namespace UI.Home
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {
            GetName();

            CollectionModule = new ReadOnlyCollection<EntityPlugin>(_pluginRegister.GetMenu());
        }

        #region PrivateField
        private readonly PluginRegister _pluginRegister = new PluginRegister();
        private ReadOnlyCollection<EntityPlugin> _collectionModule;
        private string _username = "Пользователь";
        #endregion PrivateField

        #region PublicProperties
        public ReadOnlyCollection<EntityPlugin> CollectionModule
        {
            get => _collectionModule;
            private set => Set(ref _collectionModule, value);
        }
        public string UserName
        {
            get => _username;
            private set => Set(ref _username, value);
        }
        #endregion PublicProperties

        #region PrivateMethod

        private async void GetName()
        {
            var user = await Task.Run(() =>
            {
                try
                {
                    using (var u = UserPrincipal.Current)
                        return $"{u.GivenName} {u.MiddleName}";
                }
                catch { }

                return null;
                
            }).ConfigureAwait(false);

            if (string.IsNullOrEmpty(user) == false && string.IsNullOrWhiteSpace(user) == false)
            {
                UserName = user;
            }
        }

        #endregion PrivateMethod
    }
}