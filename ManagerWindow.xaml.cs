using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Xml.Linq;

namespace footwear_wpf
{
    /// <summary>
    /// Логика взаимодействия для ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {

        private footwear_dbEntities db;

        public ManagerWindow()
        {
            InitializeComponent();

            db = new footwear_dbEntities();

            ProductsDataGrid.ItemsSource = db.products
                .Include(p => p.manufacturer)
                .Include(p => p.supplier)
                .Include(p => p.category)
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
    }
}
