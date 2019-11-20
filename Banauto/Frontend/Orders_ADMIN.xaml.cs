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
using System.Windows.Shapes;
using System.Data.SQLite;

namespace Banauto.Frontend
{
    /// <summary>
    /// Interaction logic for Orders_ADMIN.xaml
    /// </summary>
    public partial class Orders_ADMIN : Window
    {
        SQLiteConnection Connection;
        public Orders_ADMIN(SQLiteConnection Connection)
        {
            InitializeComponent();
            this.Connection = Connection;
            LoadOrder();
        }


        private void LoadOrder()
        {
            Backend.Order order = new Backend.Order();
            string sql = "select * from Orders";
            Connection.Open();
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                order.OrderID = Convert.ToString(reader[0]);
                order.Name = Convert.ToString(reader[2]);
                order.Surname = Convert.ToString(reader[3]);
                order.Phone = Convert.ToString(reader[4]);
                order.AutoID = Convert.ToString(reader[5]);
                string colour = Convert.ToString(reader[6]);
                Frontend.UC_Orders uc = new UC_Orders(order,colour);
                stackPanel.Children.Add(uc);
            }
            command.Dispose();
            Connection.Close();
        }
    }
}
