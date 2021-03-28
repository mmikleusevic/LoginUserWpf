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
    /// Interaction logic for UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : Window
    {
        UserDbEntities _db = new UserDbEntities();
        int Id;
        public UpdatePage(int ID)
        {
            Id = ID;
            InitializeComponent();
            PopulateFormFieds(Id);
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            tblUser updateUser = (from m in _db.tblUsers
                                  where m.ID == Id
                                  select m).FirstOrDefault();
            try
            {
                updateUser.FirstName = firstNameBox.Text;
                updateUser.LastName = lastNameBox.Text;
                if (oibBox.Text.Length == 11)
                {
                    updateUser.Oib = oibBox.Text;
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
                    updateUser.Email = emailBox.Text;
                }
                if(usernameBox.Text == "" || usernameBox.Text.Count() > 15)
                {
                    MessageBox.Show("Enter a valid Username");
                    usernameBox.Focus();
                }
                else
                {
                    updateUser.UserName = usernameBox.Text;
                }
                if(passwordBox.Password == "" || passwordBox.Password.Count() > 15)
                {
                    MessageBox.Show("Enter a valid Password");
                    passwordBox.Focus();
                }
                else
                {
                    updateUser.Password = passwordBox.Password;
                }
                if (dobBox.Text == "")
                {
                    updateUser.DoB = Convert.ToDateTime("1.1.2000.");
                }
                else
                {
                    updateUser.DoB = Convert.ToDateTime(dobBox.Text);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
            if (emailBox.Text != "" && oibBox.Text != "" && usernameBox.Text != "" && passwordBox.Password != "")
            {
                _db.SaveChanges();
                MainWindow.datagrid.ItemsSource = _db.tblUsers.ToList();
                this.Hide();
            }

        }
        public void PopulateFormFieds(int userID)
        {
            var result = (from o in _db.tblUsers
                          where o.ID == userID
                          select o).ToList();
            foreach (tblUser user in result)
            {
                if (result.Equals(0))
                {
                    firstNameBox.Text = string.Empty;
                    lastNameBox.Text = string.Empty;
                    oibBox.Text = string.Empty;
                    emailBox.Text = string.Empty;
                    usernameBox.Text = string.Empty;
                    passwordBox.Password = string.Empty;
                    dobBox.Text = string.Empty;
                }
                else
                {
                    firstNameBox.Text = Convert.ToString(user.FirstName);
                    lastNameBox.Text = Convert.ToString(user.LastName);
                    oibBox.Text = Convert.ToString(user.Oib);
                    emailBox.Text = Convert.ToString(user.Email);
                    usernameBox.Text = Convert.ToString(user.UserName);
                    passwordBox.Password = Convert.ToString(user.Password);
                    dobBox.Text = Convert.ToString(user.DoB);
                }
            }
            _db.SaveChanges();
        }
    }
}
