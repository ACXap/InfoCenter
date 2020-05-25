using Common.Settings;
using Common.Settings.Repository;
using Common.Settings.Service;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using UI.About;
using UI.Home;
using UI.ViewModels;

namespace UI
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IRepositorySettings, RepositoryJsonSettings>();
            SimpleIoc.Default.Register<ISettingsService, SettingsService>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<AboutViewModel>();

            PluginRegister.Register();
        }

        public ViewModelBase MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();
        public ViewModelBase SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();
        public ViewModelBase HomeViewModel => ServiceLocator.Current.GetInstance<HomeViewModel>();
        public ViewModelBase AboutViewModel => ServiceLocator.Current.GetInstance<AboutViewModel>();

        public PluginRegister PluginViewModel { get; } = new PluginRegister();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}