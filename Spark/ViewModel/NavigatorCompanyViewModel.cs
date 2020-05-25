using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace Spark.ViewModel
{
    public class NavigatorCompanyViewModel : ViewModelBase
    {
        public NavigatorCompanyViewModel(CompanySparkViewModel companyViewModel)
        {
            MainInfo = new List<string>()
            {
                "Карточка компании",
                "Регистрационные данные",
                "Виды деятельности",
                "Телефоны и Адреса",
                "История изменений наименований"
            };

            StructureCompany = new List<string>()
            {
                "Органы управления",
                "Совладельцы",
                "Сводные данные о структуре",
                "Структура (развернуто)"
            };

            ActionCompany = new List<string>()
            {
                "Баланс и отчет о фин. результатах",
                "Участие в Госконтрактах",
                "Арбитражные дела",
                "Сообщения о банкротстве",
                "Исполнительные производства",
                "Залоги"
            };

            _companyViewModel = companyViewModel;
        }

        #region PrivateField
        private readonly CompanySparkViewModel _companyViewModel;

        private List<string> _mainInfo;
        private List<string> _structureCompany;
        private List<string> _actionCompany;

        private string _currentMainInfoItem = string.Empty;
        private string _currentStructureCompanyItem = string.Empty;
        private string _currentActionCompanyItem = string.Empty;
        #endregion PrivateField

        #region PublicProperties
        public List<string> MainInfo
        {
            get => _mainInfo;
            set => Set(ref _mainInfo, value);
        }

        public List<string> StructureCompany
        {
            get => _structureCompany;
            set => Set(ref _structureCompany, value);
        }

        public List<string> ActionCompany
        {
            get => _actionCompany;
            set => Set(ref _actionCompany, value);
        }

        public string CurrentMainInfoItem
        {
            get => _currentMainInfoItem;
            set
            {
                Set(ref _currentMainInfoItem, value);
                if (value != null)
                {
                    CurrentActionCompanyItem = null;
                    CurrentStructureCompanyItem = null;

                    //if (value == "Карточка компании")
                    //{
                    //    _companyViewModel.UpdateCurrentViewMode(new CompanyViewModelMainInfo());
                    //}
                    //else if (value == "Регистрационные данные")
                    //{
                    //    _companyViewModel.UpdateCurrentViewMode(new CompanyViewModelRegistrationInfo());
                    //}
                    //else
                    //{
                    //    _companyViewModel.UpdateCurrentViewMode(new CompanyViewModelRegistrationInfo());
                    //}
                }
            }
        }

        public string CurrentStructureCompanyItem
        {
            get => _currentStructureCompanyItem;
            set
            {
                Set(ref _currentStructureCompanyItem, value);
                if (value != null)
                {
                    CurrentActionCompanyItem = null;
                    CurrentMainInfoItem = null;

                    //_companyViewModel.UpdateCurrentViewMode(new CompanyViewModelRegistrationInfo());
                }
            }

        }

        public string CurrentActionCompanyItem
        {
            get => _currentActionCompanyItem;
            set
            {
                Set(ref _currentActionCompanyItem, value);
                if (value != null)
                {
                    CurrentStructureCompanyItem = null;
                    CurrentMainInfoItem = null;

                    //_companyViewModel.UpdateCurrentViewMode(new CompanyViewModelRegistrationInfo());
                }

            }
        }
        #endregion PublicProperties

        public void SetFirstItem()
        {
            CurrentMainInfoItem = _mainInfo[0];
        }
    }
}