using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightTicketManagement.Utilities;
using System.Windows;
using FlightTicketManagement.Model;


namespace FlightTicketManagement.ViewModel
{
    class LoginVM:Utilities.ViewModelBase
    {
        public bool IsLogin { get; set; }

        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        public ICommand LoginCommand { get; set; }

        public LoginVM()
        {
            IsLogin = false;
            Password = "";
            UserName = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
        }
        void Login(Window p)
        {
            if (p == null)
                return;
            var accCount = DataProvider.Ins.DB.TAIKHOANs.Where(x => x.TenTaiKhoan == UserName && x.MatKhau == Password).Count();

            if (accCount > 0)
            {
                IsLogin = true;

                p.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }

    }
}
