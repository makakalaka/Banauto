using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Backend.User user = new Backend.User();
            user.Login(txtUsername.Text.ToLower(), txtPassword.Password);
            if (user.Username != null)
            {
                    MessageBox.Show($"Sveiki, {user.Name} !");
                    Frontend.UserWindow userWindow = new UserWindow(user);
                    this.Close();
                    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.Close();
                    userWindow.ShowDialog();
            }
            else MessageBox.Show("Neteisingas prisijungimo vardas arba stapltažodis!");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Frontend.Register register = new Register();
            register.ShowDialog();
        }
    }
}
