﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GUI.Model;
using GUI.Utilities;

namespace GUI.ViewModel
{
    class FlightsVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public string Flight
        {
            get { return _pageModel.Flight; }
            set { _pageModel.Flight = value; OnPropertyChanged(); }
        }


        public FlightsVM()
        {
            _pageModel = new PageModel();
            Flight = "hihi đây là test chuyến bay nhó";
        }
    }
}