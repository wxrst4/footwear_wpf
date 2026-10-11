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
using System.Data.Entity;

namespace footwear_wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private footwear_dbEntities db;

        public MainWindow()
        {
            InitializeComponent();

            db = new footwear_dbEntities();
        }

        private void BtnEnterGuest(object sender, RoutedEventArgs e)
        {
            new GuestWindow().ShowDialog();
        }

        private void BtnEnterWithRole(object sender, RoutedEventArgs e)
        {
            var login = tbLogin.Text.Trim();
            var password = tbPassword.Text.Trim();

            var user = db.users
                .FirstOrDefault(u => u.login == login && u.password == password);

            if (user.roles.Any(r => r.name == "Manager"))
            {
                new ManagerWindow().ShowDialog();
            }
            else if (user.roles.Any(r => r.name == "Client"))
            {
                new GuestWindow().ShowDialog();
            }
            else
            {
                new AdminWindow().ShowDialog();
            }
        }
    }
}
