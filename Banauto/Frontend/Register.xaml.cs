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

namespace Banauto.Frontend
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        string type="User";
        public Register()
        {
            InitializeComponent();
        }

        public Register(string type)
        {
            InitializeComponent();
            this.type = "Admin";
            lblAdmin.Visibility = Visibility.Visible;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtPassword1.Password != "" && txtPassword2.Password != "")
                {
                    if (txtPassword1.Password == txtPassword2.Password)
                    {
                        Backend.User user = new Backend.User();
                        user.Username = txtUsername.Text.ToLower();
                        user.Password = txtPassword1.Password;
                        user.Name = txtName.Text;
                        user.Surname = txtSurname.Text;
                        user.Phone = txtPhone.Text;
                        user.Type = this.type;
                        user.Register(user);
                        MessageBox.Show("Registracija sėkminga. Naudodami savo prisijungimo duomenis galite prisijungti prie parduotuvės.");
                        this.Close();
                        if (type == "User")
                        {
                            Frontend.Login login = new Login();
                            login.ShowDialog();
                        }
                    }
                    else throw new Exception("Įvesti slaptažodžiai nesutampa!");
                }
                else throw new Exception("Neįvestas slaptažodis!");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                txtPassword1.Clear();
                txtPassword2.Clear();
            }
        }
    }
}
