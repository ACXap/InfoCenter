#region using
using CommonServiceLocator;
using Egrul.Repository;
using Egrul.Service;
using Egrul.Service.Interfaces;
using Egrul.ViewModel;
using Egrul.Views;
using Fssp.Repository;
using Fssp.Service;
using Fssp.Service.Interface;
using Fssp.ViewModel;
using Fssp.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Spark.Repository;
using Spark.Service;
using Spark.Service.Interfaces;
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
            //SimpleIoc.Default.Register<IRepositorySpark, RepositorySparkTest>();
            SimpleIoc.Default.Register<IRepositorySpark, RepositorySparkSite>();
            SimpleIoc.Default.Register<ILoadCompanyService, LoadCompanyService>();
            SimpleIoc.Default.Register<IFoundCompanySparkService, FoundCompanyService>();
            SimpleIoc.Default.Register<FoundCompanySparkViewModel>();
            SimpleIoc.Default.Register<CompanySparkViewModel>();
            SimpleIoc.Default.Register<MainSparkViewModel>();

            //SimpleIoc.Default.Register<IRepositoryEgrul, RepositoryEgrulTest>();
            SimpleIoc.Default.Register<IRepositoryEgrul, RepositoryEgrulNalog>();
            SimpleIoc.Default.Register<IFoundCompanyEgrulService, FoundCompanyEgrulService>();
            SimpleIoc.Default.Register<FoundCompanyEgrulViewModel>();
            SimpleIoc.Default.Register<MainEgrulViewModel>();

            SimpleIoc.Default.Register<IRepositoryFssp, RepositoryFsspApi>();
            SimpleIoc.Default.Register<IFoundPersonFsspService, FoundPersonFsspService>();
            SimpleIoc.Default.Register<FoundPersonFsspViewModel>();
            SimpleIoc.Default.Register<MainFsspViewModel>();
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
                }
            };

            return list;
        }

        public static ViewModelBase MainSparkViewModel => ServiceLocator.Current.GetInstance<MainSparkViewModel>();
        public static ViewModelBase MainEgrulViewModel => ServiceLocator.Current.GetInstance<MainEgrulViewModel>();
        public static ViewModelBase MainFsspViewModel => ServiceLocator.Current.GetInstance<MainFsspViewModel>();
    }
}