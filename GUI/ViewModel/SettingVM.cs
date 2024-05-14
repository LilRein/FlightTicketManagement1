using GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModel
{
    class SettingVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public string Setting
        {
            get { return _pageModel.Setting; }
            set { _pageModel.Setting = value; OnPropertyChanged(); }
        }
        public SettingVM()
        {
            _pageModel = new PageModel();
            Setting = "hihi đây là test setting";
        }
    }
}
