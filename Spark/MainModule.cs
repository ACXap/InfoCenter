using Common;
using Common.Data;
using Common.Service;
using Spark.Repository;
using Spark.Service;
using Spark.ViewModel;
using Spark.Views;
using System.IO;

namespace Spark
{
    public class MainModule : IModule
    {
        private const string LABEL = "СПАРК";
        private const string TOOLTIP = "Поиск предприятий в базе СПАРК";
        private const string ICON = "pack://application:,,,/Spark;component/spark.png";

        private static object Initialize()
        {
            //var folder = CreateFolderModule();
            //var rep = new RepositorySparkSite();
            //var createFileService = new CreateFileOffice(folder);
            //var mainService = new FoundCompanyService(rep, createFileService);
            //var foundViewModel = new FoundCompanySparkViewModel(mainService);
            //var mainViewModel = new MainSparkViewModel(foundViewModel);
            //var view = new MainSparkView()
            //{
            //    DataContext = mainViewModel
            //};

            // return view;
            return null;
        }

        //private static string CreateFolderModule()
        //{
        //    var curDir = Directory.GetCurrentDirectory();
        //    var folder = Path.Combine(curDir + "Spark");
        //    Directory.CreateDirectory(folder);
            
        //    return folder;
        //}

        public EntityMenu GetMenu()
        {
            return new EntityMenu()
            {
                Icon = ICON,
                Label = LABEL,
                ToolTip = TOOLTIP
            };
        }

        public Plugin GetPlugin()
        {
            var view = Initialize();

            return new Plugin()
            {
                Icon = ICON,
                Label = LABEL,
                Tag = view,
                ToolTip = TOOLTIP
            };
        }
    }
}