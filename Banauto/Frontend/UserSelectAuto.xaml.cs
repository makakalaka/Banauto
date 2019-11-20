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
using System.IO;

namespace Banauto.Frontend
{
    /// <summary>
    /// Interaction logic for UserSelectAuto.xaml
    /// </summary>
    public partial class UserSelectAuto : Window
    {
        Backend.Auto auto;
        SQLiteConnection Connection;
        Backend.User user;
        Frontend.UserWindow uw;

        public UserSelectAuto(Backend.User user, Backend.Auto auto, SQLiteConnection Connection, Frontend.UserWindow uw)
        {
            InitializeComponent();
            this.auto = auto;
            this.user = user;
            this.Connection = Connection;
            this.uw = uw;
            if (user.Type == "Admin")
            {
                adminGrid.Visibility = Visibility.Visible;
            }
            else
            {
                lblColour.IsReadOnly = true;
                lblKw.IsReadOnly = true;
                lblMake.IsReadOnly = true;
                lblModel.IsReadOnly = true;
                lblPrice.IsReadOnly = true;
                txtBlockDescription.IsReadOnly = true;
            }
            SetLabels();
            LoadImages();
        }

        private void SetLabels()
        {
            lblColour.Text = auto.VehicleColour;
            lblKw.Text = auto.VehicleKw;
            lblMake.Text = auto.VehicleMake;
            lblModel.Text = auto.VehicleModel;
            lblPrice.Text = auto.VehiclePrice;
            txtBlockDescription.Text = auto.VehicleDescription;
        }

        private void LoadImages()
        {
            Connection.Open();
            stackPanel.Children.Clear();
            string sql = $"select name from '{auto.vehicleID}'";
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Frontend.UC_Images printImage = new Frontend.UC_Images(auto, Convert.ToString(reader.GetString(0)),this.user);
                stackPanel.Children.Add(printImage);
            }
            command.Dispose();
            Connection.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            //messagebox Y/N
            btnOrder.IsEnabled = false;
            Backend.Order order = new Backend.Order();
            order.NewUsername = user.Username;
            order.Name = user.Name;
            order.Surname = user.Surname;
            order.Phone = user.Phone;
            order.AutoID = this.auto.vehicleID;
            order.OrderID = Convert.ToString(DateTime.Now.Ticks);

            Backend.DatabaseUtility.SubmitOrder(order);
            MessageBox.Show($"Užsakymas sėkmingas. Su jumis susisieks mūsų konsultantas jūsų nurodytu telefono numeriu.({user.Phone})");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            //Frontend.UserWindow uw = new UserWindow(user);
            //uw.ShowDialog();
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Backend.Auto auto = new Backend.Auto();
                auto.VehicleKw = lblKw.Text;
                auto.VehicleMake = lblMake.Text;
                auto.VehicleModel = lblModel.Text;
                auto.VehicleColour = lblColour.Text;
                auto.VehicleDescription = txtBlockDescription.Text;
                auto.VehiclePrice = lblPrice.Text;
                auto.vehicleID = this.auto.vehicleID;
                auto.UpdateAuto();
                MessageBox.Show("Duomenys sėkmingai atnaujinti");

            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
        }

        private void btnDeleteAuto_Click_1(object sender, RoutedEventArgs e)
        {
            Connection.Open();
            //trina images lentelej failus
            string sql = $"select * from '{auto.vehicleID}'";
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string path = $"{Environment.CurrentDirectory}\\Images\\{Convert.ToString(reader[0])}";
                File.Delete(path);
            }
            //trina images lentele
            sql = $"drop table '{auto.vehicleID}'";
            SQLiteCommand cmd = new SQLiteCommand(sql, Connection);
            cmd.ExecuteNonQuery();
            //trina is Vehicle lenteles
            sql = $"DELETE FROM Vehicles WHERE vehicleID = '{auto.vehicleID}'";
            cmd = new SQLiteCommand(sql, Connection);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            command.Dispose();
            Connection.Close();
            MessageBox.Show("Automobilis ištrintas.");
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            uw.stackPanel.Children.Clear();
            uw.Load10();
        }
    }
}
