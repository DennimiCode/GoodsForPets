using System.Windows;

using GoodsForPets.Models;

namespace GoodsForPets.Views.Windows
{
    /// <summary>
    /// Interaction logic for AddCategoryWindow.xaml
    /// </summary>
    public partial class AddCategoryWindow : Window
    {
        private Context _context;
        public AddCategoryWindow()
        {
            InitializeComponent();
            _context = new Context();
        }

        private async void AddNewCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            Category category = new Category
            {
                Name = NameTextBox.Text,
                Unit = UnitTextBox.Text
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
    }
}
