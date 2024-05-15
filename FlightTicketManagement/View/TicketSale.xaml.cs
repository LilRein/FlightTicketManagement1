using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightTicketManagement.View
{
    /// <summary>
    /// Interaction logic for TicketSale.xaml
    /// </summary>
    public partial class TicketSale : UserControl
    {
        public TicketSale()
        {
            InitializeComponent();
        }
        private void InputFlightID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int caretIndex = textBox.CaretIndex; // Lưu vị trí của con trỏ
            textBox.Text += e.Text.ToUpper();
            textBox.CaretIndex = caretIndex + 1; // Đặt lại vị trí của con trỏ sau khi thêm ký tự vào Text
            e.Handled = true;
        }

        private void InputFlightID_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true; // Ngăn không cho sự kiện được xử lý
            }
        }
    }
}
