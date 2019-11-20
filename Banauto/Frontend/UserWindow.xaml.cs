using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
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

namespace Banauto.Frontend
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        SQLiteConnection Connection= new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + "\\DataBases\\Database.db");
        Backend.User user;
        Backend.Auto auto;

        public UserWindow(Backend.User user)
        {
            this.user = user;
            InitializeComponent();
            Load10();
            if (user.Type == "Admin")
            {
                gridAdmin.Visibility = Visibility.Visible;
                lblIfAdmin.Visibility = Visibility.Visible;
            }
            if (user.Username.ToUpper() == "ADMIN")
                btnNewAdmin.IsEnabled = true;
            lblGreeting.Content = $"Sveiki, {user.Name} {user.Surname} !";
            contactPhone.Text = user.Phone;
        }

        private void uploadImage_Click(object sender, RoutedEventArgs e)
                {
                    Backend.DatabaseUtility.UploadImage(this.auto.vehicleID, Convert.ToString(imagePathName.Content));
                    MessageBox.Show("Nuotrauka sėkmingai įkelta.");
                    imagePathName.Content = "Nuotrauka nepasirinkta...";
                    uploadImage.IsEnabled = false;
                    LoadImages(this.auto);
                }
        private void ClearTxt()
        {
            txtColour.Clear();
            txtDescription.Clear();
            txtKW.Clear();
            txtMake.Clear();
            txtModel.Clear();
            txtPrice.Clear();
            txtSearch.Clear();
            txtKW.Clear();
            txtMake.Clear();
            txtModel.Clear();
            txtColour.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
        }
        public void Load10()
        {
            stackPanel.Children.Clear();
            Connection.Open();
            string sql = "select * from Vehicles";
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            int i = 0;
            while(reader.Read())
            {
                Backend.Auto auto1 = new Backend.Auto();
                auto1.VehicleMake = Convert.ToString(reader[0]);
                auto1.VehicleModel = Convert.ToString(reader[1]);
                auto1.VehicleDescription = Convert.ToString(reader[2]);
                auto1.VehicleColour = Convert.ToString(reader[3]);
                auto1.VehicleKw = Convert.ToString(reader[4]);
                auto1.vehicleID = Convert.ToString(reader[5]);
                auto1.VehiclePrice = Convert.ToString(reader[6]);
                Frontend.UC_Autos newItem = new Frontend.UC_Autos(this.user, auto1, Connection,this);
                stackPanel.Children.Add(newItem);
                i++;
                if (i > 10)
                    break;
            }
            command.Dispose();
            Connection.Close();
        }



        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "")
                Load10();
            else
            {
                stackPanel.Children.Clear();
                Connection.Open();
                string sql = "select * from Vehicles";
                SQLiteCommand command = new SQLiteCommand(sql, Connection);
                SQLiteDataReader reader = command.ExecuteReader();
                Backend.Auto auto = new Backend.Auto();
                while (reader.Read())
                {
                    if (txtSearch.Text.ToUpper() == Convert.ToString(reader[0]).ToUpper() ||
                        txtSearch.Text.ToUpper() == Convert.ToString(reader[1]).ToUpper() ||
                        txtSearch.Text.ToUpper() == Convert.ToString(reader[2]).ToUpper() ||
                        txtSearch.Text.ToUpper() == Convert.ToString(reader[3]).ToUpper() ||
                        txtSearch.Text.ToUpper() == Convert.ToString(reader[4]).ToUpper() ||
                        txtSearch.Text.ToUpper() == Convert.ToString(reader[5]).ToUpper() ||
                        txtSearch.Text.ToUpper() == Convert.ToString(reader[6]).ToUpper())
                    {
                        auto.VehicleMake = Convert.ToString(reader[0]);
                        auto.VehicleModel = Convert.ToString(reader[1]);
                        auto.VehicleDescription = Convert.ToString(reader[2]);
                        auto.VehicleColour = Convert.ToString(reader[3]);
                        auto.VehicleKw = Convert.ToString(reader[4]);
                        auto.vehicleID = Convert.ToString(reader[5]);
                        auto.VehiclePrice = Convert.ToString(reader[6]);
                        Frontend.UC_Autos newItem = new Frontend.UC_Autos(user, auto, Connection,this);
                        stackPanel.Children.Add(newItem);
                    }
                }
                command.Dispose();
                Connection.Close();
            }
        }

        private void btnAdminAddNewAuto_Click(object sender, RoutedEventArgs e)
        {
            if(user.Type=="Admin")
            {
                gridAdminNewAuto.Visibility = Visibility.Visible;
            }
        }

        private void btnSaveNewAuto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Backend.Auto auto = new Backend.Auto();
                auto.VehicleKw = txtKW.Text;
                auto.VehicleMake = txtMake.Text;
                auto.VehicleModel = txtModel.Text;
                auto.VehicleColour = txtColour.Text;
                auto.VehicleDescription = txtDescription.Text;
                auto.VehiclePrice = txtPrice.Text;
                auto.AddAuto();
                MessageBox.Show("Automobilis sėkmingai išsaugotas. Galima ikelti automobilio nuotraukas.");
                newVehicleImageStackpanel.Children.Clear();
                gridAdminNewAuto.Visibility = Visibility.Collapsed;
                ClearTxt();
                this.auto = auto;
                gridNewVehicleImages.Visibility = Visibility.Visible;
                Load10();
            }
            catch(Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
        }

        private void btnCancelNewAuto_Click(object sender, RoutedEventArgs e)
        {
            gridAdminNewAuto.Visibility = Visibility.Collapsed;
            ClearTxt();
            Load10();
        }

        private void searchForPhoto_Click(object sender, RoutedEventArgs e)
        {
                OpenFileDialog imageSearch = new OpenFileDialog();
                imageSearch.InitialDirectory = "c:\\";
                imageSearch.Filter = "Nuotraukos (*.jpg, *.png)|*.jpg;*.png";
                imageSearch.FilterIndex = 0;
                imageSearch.RestoreDirectory = true;

                if (imageSearch.ShowDialog() == true)
                {
                    imagePathName.Content = imageSearch.FileName;
                    uploadImage.IsEnabled = true;
                }
        }
 
        private void LoadImages(Backend.Auto auto)
        {
            Connection.Open();
            newVehicleImageStackpanel.Children.Clear();
            string sql = $"select name from '{auto.vehicleID}'";
            SQLiteCommand command = new SQLiteCommand(sql, Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Frontend.UC_Images printImage = new Frontend.UC_Images(auto, Convert.ToString(reader.GetString(0)), this.user);
                newVehicleImageStackpanel.Children.Add(printImage);
            }
            command.Dispose();
            Connection.Close();
        }

        private void finishSave_Click(object sender, RoutedEventArgs e)
        {
            ClearTxt();
            gridNewVehicleImages.Visibility = Visibility.Collapsed;
            Load10();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void saveContactPhone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                user.Phone = contactPhone.Text;
                Backend.DatabaseUtility.ChangePhone(user);
                MessageBox.Show("Telefono numeris atnaujintas.");
            }
            catch(Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
        }

        private void btnOrdersList_Click(object sender, RoutedEventArgs e)
        {
            Frontend.Orders_ADMIN oa = new Orders_ADMIN(Connection);
            oa.ShowDialog();
        }

        private void btnNewAdmin_Click(object sender, RoutedEventArgs e)
        {
            Frontend.Register register = new Register("Admin");
            register.ShowDialog();
        }
    }
}
