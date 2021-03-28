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
using System.Windows.Navigation;

namespace LoginUserWpf
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        UserDbEntities _db = new UserDbEntities();
        public Login()
        {
            InitializeComponent();
        }
        private bool VerifyUser(string Username, string Password)
        {
            tblUser updateUser = (from m in _db.tblUsers
                                  where m.UserName == Username && m.Password == Password
                                  select m).FirstOrDefault();

            if (updateUser != null)
            {
                if (updateUser.UserName.Equals(Username) && updateUser.Password.Equals(Password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                if (VerifyUser(txtUsername.Text, txtPassword.Password))
                {
                    MessageBox.Show("Prijava uspješna", "Dobrodošli", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Korisnicko ime ili lozinka su netočni", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _db.SaveChanges();               
            }
        }
    }
}
