using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FlightTicketManagement.Utilities;
using FlightTicketManagement.Model; // Adjust this namespace to match your actual namespace

namespace FlightTicketManagement.ViewModel
{
    class LoginVM : ViewModelBase
    {
        private string _username;
        private string _password;
        private ICommand _loginCommand;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(param => this.Login(), param => this.CanLogin());
                }
                return _loginCommand;
            }
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        public event Action LoginSuccess;

        private void Login()
        {
            using (var context = new FLIGHTTICKETMANAGEMENTEntities())
            {
                var user = context.TAIKHOANs.SingleOrDefault(u => u.TenTaiKhoan == Username && u.MatKhau == Password);

                if (user != null)
                {
                    MessageBox.Show("Login thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoginSuccess?.Invoke();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản và mật khẩu", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
