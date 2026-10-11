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
using System.Data.Entity; 

namespace footwear_wpf
{
    /// <summary>
    /// Логика взаимодействия для GuestWindow.xaml
    /// </summary>
    public partial class GuestWindow : Window
    {

        private footwear_dbEntities db;

        public GuestWindow()
        {
            InitializeComponent();

            db = new footwear_dbEntities();

            ProductsDataGrid.ItemsSource = db.products
                .Include(p => p.category)
                .Include(p => p.manufacturer)
                .Include(p => p.supplier)
                .ToList();
        }

    }
}
