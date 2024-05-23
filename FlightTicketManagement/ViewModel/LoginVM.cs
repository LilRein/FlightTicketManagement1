using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightTicketManagement.Utilities;
using System.Windows;
using FlightTicketManagement.Model;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;


namespace FlightTicketManagement.ViewModel
{
    class LoginVM : ViewModelBase
    {
        public bool IsLogin { get; private set; }

        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(nameof(UserName)); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(nameof(Password)); } }
        public ICommand LoginCommand { get; }
        public ICommand PasswordChangedCommand { get; set; }
        public LoginVM()
        {
            IsLogin = false;
            Password = "";
            UserName = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }
        private void Login(Window p)
        {
            if (p == null)
                return;

            string passEncode = MD5Hash(Base64Encode(Password));
            var accCount = DataProvider.Ins.DB.TAIKHOANs.Where(x => x.TenTaiKhoan == UserName && x.MatKhau == Password).Count();

            if (accCount > 0)
            {
                IsLogin = true;

                p.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}