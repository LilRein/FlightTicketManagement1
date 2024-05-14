using GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModel
{
    class ReportsVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public string Report
        {
            get { return _pageModel.Report; }
            set { _pageModel.Report = value; OnPropertyChanged(); }
        }
        public ReportsVM()
        {
            _pageModel = new PageModel();
            Report = "hihi đây là test báo cáo";
        }
    }
}
