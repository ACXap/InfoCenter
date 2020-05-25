using Common.Data;
using GalaSoft.MvvmLight;
using Spark.Data.Model;
using Spark.Service.Interfaces;

namespace Spark.ViewModel
{
    public class CompanySparkViewModel : ViewModelBase
    {
        public CompanySparkViewModel(ILoadCompanyService loadCompanyService)
        {
            _loadCompanyService = loadCompanyService;
            Navigator = new NavigatorCompanyViewModel(this);
        }

        #region PrivateField
        private readonly ILoadCompanyService _loadCompanyService;

        public NavigatorCompanyViewModel Navigator { get; private set; }
        //private IViewModelCompanyDetal _currentViewModel;
        private Company _company;
        private ErrorResult _errorStatus;


        private bool _IsShowProgressBarFound = false;
        private string _textStatusFound = "";
        #endregion PrivateField

        #region PublicProperties
        //public IViewModelCompanyDetal CurrentViewModel
        //{
        //    get => _currentViewModel;
        //    set => Set(ref _currentViewModel, value);
        //}
        public Company Company
        {
            get => _company;
            set => Set(ref _company, value);
        }
        public ErrorResult ErrorStatus
        {
            get => _errorStatus;
            set => Set(ref _errorStatus, value);
        }
        public bool IsShowProgressBarFound
        {
            get => _IsShowProgressBarFound;
            set => Set(ref _IsShowProgressBarFound, value);
        }
        public string TextStatusFound
        {
            get => _textStatusFound;
            set => Set(ref _textStatusFound, value);
        }
        #endregion PublicProperties

        #region PrivateMethod
        #endregion PrivateMethod

        #region PublicMethod
        public async void LoadCard(CompanyInfo company)
        {
            //Navigator.SetFirstItem();
            //CurrentViewModel = null;
            TextStatusFound = "";

            IsShowProgressBarFound = true;

            var result = await _loadCompanyService.GetCompany(company.Link);

            Company = result.Object;
            ErrorStatus = result.ErrorResult;

           // if (_company != null) UpdateCurrentViewMode(new CompanyViewModelMainInfo());
            IsShowProgressBarFound = false;
        }

        //public void UpdateCurrentViewMode(IViewModelCompanyDetal viewModel)
        //{
        //    viewModel.SetCompany(_company);
        //    CurrentViewModel = viewModel;
        //}
        #endregion PublicMethod
    }
}