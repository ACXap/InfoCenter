using GalaSoft.MvvmLight;
using System.DirectoryServices.AccountManagement;

namespace Core.Home
{
    public class HomeViewModel:ViewModelBase
    {
        public HomeViewModel()
        {
            using (var context = new PrincipalContext(ContextType.Machine))
            {
                using (var user = new UserPrincipal(context))
                {
                    using (var searcher = new PrincipalSearcher(user))
                    {
                        using (var u = searcher.FindOne() as UserPrincipal)
                        {
                            if (string.IsNullOrEmpty(u.DisplayName))
                            {
                                UserName = "Пользователь";
                            }
                            else
                            {
                                UserName = u.DisplayName;
                            }
                        }                  
                    }
                }                            
            }              
        }

        private string _username;
        public string UserName
        {
            get => _username;
            set => Set(ref _username, value);
        }
    }
}