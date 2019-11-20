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
using System.Data.SQLite;

namespace Banauto.Frontend
{
    /// <summary>
    /// Interaction logic for UC_Orders.xaml
    /// </summary>
    public partial class UC_Orders : UserControl
    {
        SQLiteConnection Connection = Backend.DatabaseUtility.Connection;
        string colour;

        public UC_Orders(Backend.Order order , string colour)
        {
            InitializeComponent();
            this.colour = colour;
            LoadOrder(order);
            UpdateColour();

        }
        private void UpdateColour()
        {
            if (colour == "1")
                gridStatus.Background = new SolidColorBrush(Colors.Green);
            else gridStatus.Background = new SolidColorBrush(Colors.Red);
        }

        private void LoadOrder(Backend.Order order)
        {
            lblOrderID.Content = order.OrderID;
            lblName.Content = order.Name;
            lblSurname.Content = order.Surname;
            lblPhone.Content = order.Phone;
            lblAutoID.Content = order.AutoID;
        }

        private void UpdateDb()
        {
            Connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("UPDATE Orders SET status=@status Where ID = @ID", Connection);
            cmd.Parameters.AddWithValue("@status", colour);
            cmd.Parameters.AddWithValue("@ID", lblOrderID.Content);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            Connection.Close();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (colour == "0")
                colour = "1";
            else colour = "0";
            UpdateColour();
            UpdateDb();
        }
    }
}
