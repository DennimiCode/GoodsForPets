using System.Windows;

using GoodsForPets.Models;

namespace GoodsForPets.Views.Windows
{
    /// <summary>
    /// Interaction logic for AddManufacturerWindow.xaml
    /// </summary>
    public partial class AddManufacturerWindow : Window
    {
        private Context _context;
        public AddManufacturerWindow()
        {
            InitializeComponent();
            _context = new Context();
        }

        private async void AddNewManufacturerButton_Click(object sender, RoutedEventArgs e)
        {
            Manufacturer manufacturer = new Manufacturer
            {
                Name = NameTextBox.Text
            };
            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();
            MessageBox.Show("Новый производитель успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Question);
            Close();
        }
    }
}
