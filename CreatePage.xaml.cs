using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoginUserWpf
{
    /// <summary>
    /// Interaction logic for CreatePage.xaml
    /// </summary>
    public partial class CreatePage : Window
    {
        UserDbEntities _db = new UserDbEntities();

        public CreatePage()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            tblUser newUser = new tblUser();
            try
            {
                newUser.FirstName = firstNameBox.Text;
                newUser.LastName = lastNameBox.Text;
                if (oibBox.Text.Length == 11)
                {
                    newUser.Oib = oibBox.Text;
                }
                else
                {
                    MessageBox.Show("Enter a valid oib");
                    oibBox.Focus();
                }         
                if (emailBox.Text.Length == 0)
                {
                    MessageBox.Show("Enter an email");
                    emailBox.Focus();
                }
                else if (!Regex.IsMatch(emailBox.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                {
                    MessageBox.Show("Enter a valid email");
                    emailBox.Select(0, emailBox.Text.Length);
                    emailBox.Focus();
                }
                else
                {
                    newUser.UserName = emailBox.Text;
                }
                if (usernameBox.Text == "" || usernameBox.Text.Count() > 15)
                {
                    MessageBox.Show("Enter a valid Username");
                    usernameBox.Focus();
                }
                else
                {
                    newUser.UserName = usernameBox.Text;
                }
                if (passwordBox.Password == "" || passwordBox.Password.Count() > 15)
                {
                    MessageBox.Show("Enter a valid Password");
                    passwordBox.Focus();
                }
                else
                {
                    newUser.Password = passwordBox.Password;
                }
                if (dobBox.Text == "")
                {
                    newUser.DoB = Convert.ToDateTime("1.1.2000.");
                }
                else
                {
                    newUser.DoB = Convert.ToDateTime(dobBox.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (emailBox.Text != "" && oibBox.Text != "" && usernameBox.Text != "" && passwordBox.Password != "")
            {
                _db.tblUsers.Add(newUser);
                _db.SaveChanges();
                MainWindow.datagrid.ItemsSource = _db.tblUsers.ToList();
                this.Hide();
            }
        }
    }
}
