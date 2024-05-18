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

        public ICommand _LoginCommand { get; set; }

        public LoginVM() 
        {
            _LoginCommand = new RelayCommand<object>((p) => true, (p) => LoginCommand());
        }

        private void LoginCommand()
        {
            MessageBox.Show("Clicked");
        }
    }
}
