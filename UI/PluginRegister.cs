#region using
using CommonServiceLocator;
using Egrul.Repository;
using Egrul.Service;
using Egrul.ViewModel;
using Egrul.Views;
using Fssp.Repository;
using Fssp.Service;
using Fssp.ViewModel;
using Fssp.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Rosreestr.Repository;
using Rosreestr.Service;
using Rosreestr.Service.Interface;
using Rosreestr.Views;
using Rosreestr.ViewModel;
using Spark.Repository;
using Spark.Service;
using Spark.ViewModel;
using Spark.Views;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using UI.Data;
#endregion using

namespace UI
{
    public class PluginRegister
    {
        public static void Register()
        {
            SimpleIoc.Default.Register<IRepositorySpark, RepositorySparkTest>();
            //SimpleIoc.Default.Register<IRepositorySpark, RepositorySparkSite>();
            SimpleIoc.Default.Register<IFoundCompanySparkService, FoundCompanyService>();
            SimpleIoc.Default.Register<FoundCompanySparkViewModel>();
            SimpleIoc.Default.Register<MainSparkViewModel>();

            //SimpleIoc.Default.Register<IRepositoryEgrul, RepositoryEgrulTest>();
            SimpleIoc.Default.Register<IRepositoryEgrul, RepositoryEgrulNalog>();
            SimpleIoc.Default.Register<IFoundCompanyEgrulService, FoundCompanyEgrulService>();
            SimpleIoc.Default.Register<FoundCompanyEgrulViewModel>();
            SimpleIoc.Default.Register<MainEgrulViewModel>();

            SimpleIoc.Default.Register<IRepositoryFssp, RepositoryFsspApi>();
            SimpleIoc.Default.Register<IFoundFsspService, FoundFsspService>();
            SimpleIoc.Default.Register<FoundPersonFsspViewModel>();
            SimpleIoc.Default.Register<FoundCompanyFsspViewModel>();
            SimpleIoc.Default.Register<FoundNumberFsspViewModel>();
            SimpleIoc.Default.Register<FoundListFsspViewModel>();
            SimpleIoc.Default.Register<MainFsspViewModel>();


            SimpleIoc.Default.Register<IRepositoryRosreestr, RepositoryRosreestrApi>();
            SimpleIoc.Default.Register<IFoundRosreestrService, FoundServiceRosresstr>();
            SimpleIoc.Default.Register<FoundNumberRosreestrViewModel>();
            SimpleIoc.Default.Register<FoundListRosreestrViewModel>();
            SimpleIoc.Default.Register<MainRosreestrViewModel>();
        }

        public List<EntityPlugin> GetPlugin()
        {
            var list = new List<EntityPlugin>()
            {
                new EntityPlugin()
                {
                    Id = 1,
                    Icon = new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/Spark;component/spark.png")) },
                    Label = "СПАРК",
                    ToolTip = "Поиск предприятий в базе СПАРК",
                    Tag = new MainSparkView()
                },
                new EntityPlugin()
                {
                    Id = 2,
                    Icon = new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/Egrul;component/egrul.png")) },
                    Label = "ЕГРЮЛ",
                    ToolTip = "Представление сведений из ЕГРЮЛ/ЕГРИП",
                    Tag = new MainEgrulView()
                },
                new EntityPlugin()
                {
                    Id = 3,
                    Icon = new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/Fssp;component/fssp.png")) },
                    Label = "ФССП",
                    ToolTip = "Федеральная служба судебных приставов",
                    Tag = new MainFsspView()
                },
                new EntityPlugin()
                {
                    Id = 4,
                    Icon = new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/Rosreestr;component/rosreestr.png")) },
                    Label = "Росреестр",
                    ToolTip = "Федеральная служба государственной регистрации, кадастра и картографии",
                    Tag = new MainRosreestrView()
                }
            };

            return list;
        }

        public List<EntityPlugin> GetMenu()
        {
            var list = new List<EntityPlugin>()
            {
                new EntityPlugin()
                {
                    Id = 1,
                    Icon = new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/Spark;component/spark.png")) },
                    Label = "СПАРК",
                    ToolTip = "Поиск предприятий в базе СПАРК"
                },
                new EntityPlugin()
                {
                    Id = 2,
                    Icon = new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/Egrul;component/egrul.png")) },
                    Label = "ЕГРЮЛ",
                    ToolTip = "Представление сведений из ЕГРЮЛ/ЕГРИП"
                },
                new EntityPlugin()
                {
                    Id = 3,
                    Icon = new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/Fssp;component/fssp.png")) },
                    Label = "ФССП",
                    ToolTip = "Федеральная служба судебных приставов"
                },
                new EntityPlugin()
                {
                    Id = 4,
                    Icon = new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/Rosreestr;component/rosreestr.png")) },
                    Label = "Росреестр",
                    ToolTip = "Федеральная служба государственной регистрации, кадастра и картографии"
                }
            };

            return list;
        }

        public static ViewModelBase MainSparkViewModel => ServiceLocator.Current.GetInstance<MainSparkViewModel>();
        public static ViewModelBase MainEgrulViewModel => ServiceLocator.Current.GetInstance<MainEgrulViewModel>();
        public static ViewModelBase MainFsspViewModel => ServiceLocator.Current.GetInstance<MainFsspViewModel>();
        public static ViewModelBase MainRosreestrViewModel => ServiceLocator.Current.GetInstance<MainRosreestrViewModel>();
    }
}