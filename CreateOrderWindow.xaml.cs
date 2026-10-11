using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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

namespace footwear_wpf
{
    /// <summary>
    /// Логика взаимодействия для CreateOrderWindow.xaml
    /// </summary>
    public partial class CreateOrderWindow : Window
    {

        private footwear_dbEntities db;

        public CreateOrderWindow()
        {
            InitializeComponent();

            db = new footwear_dbEntities();

            cbAddress.ItemsSource = db.addresses.ToList();
            cbArticleProcut.ItemsSource = db.products.ToList();
            cbUser.ItemsSource = db.users.ToList();
        }

        private void BtnCreateOrder(object sender, RoutedEventArgs e)
        {
            var order = new order
            {
                article_products = (int) cbArticleProcut.SelectedValue,
                created_at = DateTime.Now,
                delivered_at = DateTime.Now.AddDays(7),
                address_id = (int) cbAddress.SelectedValue,
                user_id = (int) cbUser.SelectedValue,
                code = new Random().Next(1, 1000),
                status = "Created"
            };

            db.orders.Add(order);
            db.SaveChanges();

            Close();
        }
    }
}
