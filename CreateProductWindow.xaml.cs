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

namespace footwear_wpf
{
    /// <summary>
    /// Логика взаимодействия для CreateProductWindow.xaml
    /// </summary>
    public partial class CreateProductWindow : Window
    {
       
        private footwear_dbEntities db;

        public CreateProductWindow()
        {
            InitializeComponent();

            db = new footwear_dbEntities();

            cbCategory.ItemsSource = db.categories.ToList();
            cbManufacture.ItemsSource = db.manufacturers.ToList();
            cbSupplier.ItemsSource = db.suppliers.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var product = new product
            {
                product_name = tbProductName.Text,
                unit = tbUnit.Text,
                price = decimal.Parse(tbPrice.Text),
                supplier_id = (int)cbSupplier.SelectedValue,
                manufacture_id = (int)cbManufacture.SelectedValue,
                category_id = (int)cbCategory.SelectedValue,
                discount = int.Parse(tbDiscount.Text),
                stock = int.Parse(tbStock.Text),
                description = tbDescription.Text
            };

            db.products.Add(product);
            db.SaveChanges();

            Close();
        }
    }
}
