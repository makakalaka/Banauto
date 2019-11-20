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
using System.IO;

namespace Banauto.Frontend
{
    /// <summary>
    /// Interaction logic for UC_Images.xaml
    /// </summary>
    public partial class UC_Images : UserControl
    {
        Backend.Auto auto;
        string imageName;
        Backend.User user;

        public UC_Images(Backend.Auto auto, string imageName, Backend.User user)
        {
            InitializeComponent();
            this.auto = auto;
            this.imageName = imageName;
            this.user = user;
            SetImage();
        }

        private void SetImage()
        {
            MemoryStream ms = new MemoryStream();
            BitmapImage bi = new BitmapImage();
            byte[] bytArray = File.ReadAllBytes(Environment.CurrentDirectory + "\\Images\\" + imageName);
            ms.Write(bytArray, 0, bytArray.Length); ms.Position = 0;
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();
            imgAuto.Source = bi;
        }
        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            if (user.Type == "Admin")
            {

            }
            else
            {

            }
        }

    }
}
