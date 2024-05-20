using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlightTicketManagement.Utilities;
using System.Windows;


namespace FlightTicketManagement.ViewModel
{
    class LoginVM:Utilities.ViewModelBase
    {
        public bool IsLogin { get; set; }
        public ICommand DashboardCommand { get; set; }

        public LoginVM() 
        {
            DashboardCommand = new RelayCommand<object>((p) => true, (p) => LoginCommand());
        }

        private void LoginCommand()
        {
            MessageBox.Show("Clicked");
        }
    }
}
