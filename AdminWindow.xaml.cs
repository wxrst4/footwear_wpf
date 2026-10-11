using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {

        private footwear_dbEntities db;

        public AdminWindow()
        {
            InitializeComponent();

            db = new footwear_dbEntities();

            ProductsDataGrid.ItemsSource = db.products
                .Include(p => p.manufacturer)
                .Include(p => p.supplier)
                .Include(p => p.category)
                .ToList();

            OrdersDataGrid.ItemsSource = db.orders
                .Include(o => o.product)
                .Include(o => o.address)
                .Include(o => o.user)
                .ToList();
        }

        private void BtnSearchProductByName(object sender, RoutedEventArgs e)
        {
            var name = tbProductName.Text.Trim();

            ProductsDataGrid.ItemsSource = db.products
                .Include(p => p.manufacturer)
                .Include(p => p.supplier)
                .Include(p => p.category)
                .Where(p => p.product_name == name)
                .ToList();

            tbProductName.Clear();
        }

        private void BtnOrderByProductByManufacturer(object sender, RoutedEventArgs e)
        {
            ProductsDataGrid.ItemsSource = db.products
                .Include(p => p.manufacturer)
                .Include(p => p.supplier)
                .Include(p => p.category)
                .OrderBy(p => p.manufacturer.name)
                .ToList();
        }

        private void BtnFilterProductByPrice(object sender, RoutedEventArgs e)
        {
            ProductsDataGrid.ItemsSource = db.products
                .Include(p => p.manufacturer)
                .Include(p => p.supplier)
                .Include(p => p.category)
                .Where(p => p.price > 1000)
                .ToList();
        }

        private void BtnResetDataToDataGrid(object sender, RoutedEventArgs e)
        {
            ProductsDataGrid.ItemsSource = db.products
                .Include(p => p.manufacturer)
                .Include(p => p.supplier)
                .Include(p => p.category)
                .ToList();
        }

        private void BtnSearchOrderByStatus(object sender, RoutedEventArgs e)
        {
            var status = tbOrderStatus.Text.Trim();

            OrdersDataGrid.ItemsSource = db.orders
                .Include(o => o.product)
                .Include(o => o.address)
                .Include(o => o.user)
                .Where(o => o.status == status)
                .ToList();
        }

        private void BtnFilterOrderByCreatedAt(object sender, RoutedEventArgs e)
        {
            OrdersDataGrid.ItemsSource = db.orders
                .Include(o => o.product)
                .Include(o => o.address)
                .Include(o => o.user)
                .OrderBy(o => o.created_at)
                .ToList();
        }

        private void BntFilterOrderByDeliveredAt(object sender, RoutedEventArgs e)
        {
            OrdersDataGrid.ItemsSource = db.orders
                .Include(o => o.product)
                .Include(o => o.address)
                .Include(o => o.user)
                .OrderBy(o => o.delivered_at)
                .ToList();
        }

        private void BtnDeleteProductById(object sender, RoutedEventArgs e)
        {
            var id = int.Parse(tbProductId.Text);

            var product = db.products.FirstOrDefault(p => p.article == id);

            db.products.Remove(product);
            db.SaveChanges();

            ProductsDataGrid.ItemsSource = db.products
                .Include(p => p.manufacturer)
                .Include(p => p.supplier)
                .Include(p => p.category)
                .ToList();

            tbProductId.Clear();
        }

        private void BtnDeleteOrderById(object sender, RoutedEventArgs e)
        {
            var id = int.Parse(tbOrderId.Text);

            var order = db.orders.FirstOrDefault(o => o.id == id);

            db.orders.Remove(order);
            db.SaveChanges();

            OrdersDataGrid.ItemsSource = db.orders
                .Include(o => o.product)
                .Include(o => o.address)
                .Include(o => o.user)
                .ToList();

            tbOrderId.Clear();
        }

        private void BtnCreateProduct(object sender, RoutedEventArgs e)
        {
            new CreateProductWindow().ShowDialog();

            ProductsDataGrid.ItemsSource = db.products
                .Include(p => p.manufacturer)
                .Include(p => p.supplier)
                .Include(p => p.category)
                .ToList();
        }

        private void BntCreateOrder(object sender, RoutedEventArgs e)
        {
            new CreateOrderWindow().ShowDialog();

            OrdersDataGrid.ItemsSource = db.orders
                .Include(o => o.product)
                .Include(o => o.address)
                .Include(o => o.user)
                .ToList();
        }
    }
}

