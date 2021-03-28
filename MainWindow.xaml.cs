using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Data;

namespace LoginUserWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserDbEntities _db = new UserDbEntities();
        public static DataGrid datagrid;

        public MainWindow()
        {
            InitializeComponent();
            Load();
        }

        private void Load()
        {
            UserDataGrid.ItemsSource = _db.tblUsers.ToList();
            datagrid = UserDataGrid;
        }

        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            CreatePage Ipage = new CreatePage();
            Ipage.ShowDialog();
        }

        private void UpdateUserButton_Click(object sender, RoutedEventArgs e)
        {
            int Id = (UserDataGrid.SelectedItem as tblUser).ID;
            UpdatePage Upage = new UpdatePage(Id);
            Upage.PopulateFormFieds(Id);
            Upage.ShowDialog();
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            int Id = (UserDataGrid.SelectedItem as tblUser).ID;
            var deleteUser = _db.tblUsers.Where(m => m.ID == Id).Single();
            _db.tblUsers.Remove(deleteUser);
            _db.SaveChanges();
            UserDataGrid.ItemsSource = _db.tblUsers.ToList();
        }

        private void UserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUserButton.IsEnabled = true;
            DeleteUserButton.IsEnabled = true;
        }

        private void PdfButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if(printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(UserDataGrid, "Users");
            }
        }
    }
}
