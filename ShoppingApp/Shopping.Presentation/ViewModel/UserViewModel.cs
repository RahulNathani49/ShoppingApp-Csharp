using Shopping.Domain.Services;
using Shopping.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Presentation.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        
        public DelegateCommand LoginCommand { get; }

        private Service service;
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

        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                NotifyPropertyChanged(nameof(Message));
            }
        }



        public UserViewModel()
        {
            LoginCommand = new DelegateCommand(Login);
            service = new Service();
        }
        private void Login(object parameter)
        {

            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                bool check = service.getPassword(UserName,Password);
                if(check)
                {
                    Message = "Successfully Logged In !!";
                }
                else
                {
                    Message = "Invalid credentials XXXX";
                }

            }
        }
        private void NotifyPropertyChanged(string parameterName)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(parameterName));
        }
    }
}
