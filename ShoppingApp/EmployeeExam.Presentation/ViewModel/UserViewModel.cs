using EmployeeExam.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeExam.Presentation.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        
        public DelegateCommand LoginCommand { get; }

        //private UserRepository
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value;
                LoginCommand.NotifyCanExecuteChanged();
                NotifyPropertyChanged(nameof(UserName));
                
                    }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value;
                LoginCommand.NotifyCanExecuteChanged();
                NotifyPropertyChanged(nameof(Password));
            }
        }

        
        public UserViewModel()
        {
            LoginCommand = new DelegateCommand(Login);
        }
        private void Login(object parameter)
        {

            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
               
            }
        }
        private void NotifyPropertyChanged(string parameterName)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(parameterName));
        }
    }
}
