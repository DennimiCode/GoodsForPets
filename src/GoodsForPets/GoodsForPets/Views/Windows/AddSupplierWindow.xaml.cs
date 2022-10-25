using System.Windows;

using GoodsForPets.Models;

namespace GoodsForPets.Views.Windows
{
    /// <summary>
    /// Interaction logic for AddSupplierWindow.xaml
    /// </summary>
    public partial class AddSupplierWindow : Window
    {
        private Context _context;
        public AddSupplierWindow()
        {
            InitializeComponent();
            _context = new Context();
        }

        private async void AddNewSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            Supplier supplier = new Supplier
            {
                Name = NameTextBox.Text
            };
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
        }
    }
}
