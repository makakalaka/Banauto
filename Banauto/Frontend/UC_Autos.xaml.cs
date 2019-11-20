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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Banauto.Frontend
{
    /// <summary>
    /// Interaction logic for UC_Autos.xaml
    /// </summary>
    public partial class UC_Autos : UserControl
    {
        Backend.Auto auto;
        Backend.User user;
        SQLiteConnection Connection;
        Frontend.UserWindow uw;




        public UC_Autos(Backend.User user, Backend.Auto auto, SQLiteConnection Connection,Frontend.UserWindow uw)
        {        
            InitializeComponent();
            this.auto = auto;
            this.Connection = Connection;
            this.user = user;
            this.uw = uw;
            if (this.user.Type == "User")//jei useris nerodo ID
                lblVehicleID.Visibility = Visibility.Collapsed;

            try 
            { 
                string sql = $"select name from '{auto.vehicleID}'";
                SQLiteCommand command = new SQLiteCommand(sql, Connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string imageName = reader.GetString(0);
                    MemoryStream ms = new MemoryStream();
                    BitmapImage bi = new BitmapImage();
                    byte[] bytArray = File.ReadAllBytes(Environment.CurrentDirectory + "\\Images\\" + imageName);
                    ms.Write(bytArray, 0, bytArray.Length); ms.Position = 0;
                    bi.BeginInit();
                    bi.StreamSource = ms;
                    bi.EndInit();
                    imgAuto.Source = bi;
                    command.Dispose();
                    break;
                }
            }
            catch(Exception exe)
            {
                MessageBox.Show(exe.Message);
                ImageSource itemImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Images\\imgDefault.png")); //default img
                imgAuto.Source = itemImage;
            }
            lblAutoMake_Model.Content = auto.VehicleMake + " " + auto.VehicleModel;
            lblVehicleID.Content = auto.vehicleID;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            Frontend.UserSelectAuto window = new UserSelectAuto(user, auto, Connection,uw);
            window.ShowDialog();
        }
    }
}
