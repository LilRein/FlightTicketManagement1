using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlightTicketManagement.View.Components
{
    /// <summary>
    /// Interaction logic for AddFlight.xaml
    /// </summary>
    public partial class AddFlight : UserControl
    {
        public AddFlight()
        {
            InitializeComponent();
        }

        private void InputFlightID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int caretIndex = textBox.CaretIndex; // Lưu vị trí của con trỏ
            textBox.Text = textBox.Text.Insert(caretIndex, e.Text.ToUpper());
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}