using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

using GoodsForPets.Models;

using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;

namespace GoodsForPets.Views.Windows
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private readonly ProductRecord _product;
        private readonly bool _isEdit;
        private Context _context;
        public ProductWindow(ProductRecord product, bool isEdit = true)
        {
            InitializeComponent();
            _product = product;
            _isEdit = isEdit;
            _context = new Context();
        }

        private void ProductWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isEdit)
            {
                Title = "Редактирование товара";
                SaveCreateProductButton.Content = "Сохранить";
                ArticleNumerTextBox.IsReadOnly = true;

                if (_product != null)
                {
                    ArticleNumerTextBox.Text = _product.ArticleNumber;
                    NameTextBox.Text = _product.Name;
                    CostTextBox.Text = _product.Cost.ToString();
                    MaxDiscountAmountTextBox.Text = _product.MaxDiscountAmount.ToString();
                    CurrentDiscountAmountTextBox.Text = _product.CurrentDiscountAmount.ToString();
                    QuantityInStockTextBox.Text = _product.QuantityInStock.ToString();
                    DescriptionTextBlock.Text = _product.Description;
                    ProductImage.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + $"\\Resources\\ItemsImages\\{(_product.Photo == null ? "emptyItem.png" : _product.Photo)}", UriKind.Absolute));
                }
            }
            else
            {
                Title = "Добавление товара";
                SaveCreateProductButton.Content = "Добавить";
                DeleteProductButton.Visibility = Visibility.Hidden;
            }
        }

        private void AddSupplierButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddManufacturerButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddProductImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = SpecialDirectories.MyPictures;
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Выбрать фото";
            openFileDialog.Filter = "All images types (*.png,*.jpg,*.jpeg,*.bmp,*.gif)|*.png;*.jpg;*.jpeg;*.bmp;*.gif|PNG (*.png)|*.png|JPG (*.jpg)|*.jpg|JPEG (*.jpeg)|*.jpeg|BMP (*.bmp)|*.bmp|GIF (*.gif)|*.gif";

            if (openFileDialog.ShowDialog() == true && !string.IsNullOrWhiteSpace(ArticleNumerTextBox.Text))
            {
                string fileExtension = Path.GetExtension(openFileDialog.FileName);
                File.Copy(openFileDialog.FileName, Environment.CurrentDirectory + $"\\Resources\\ItemsImages\\{ArticleNumerTextBox.Text}{fileExtension}", true);
                File.Copy(openFileDialog.FileName, Environment.CurrentDirectory + $"\\..\\..\\..\\Resources\\ItemsImages\\{ArticleNumerTextBox.Text}{fileExtension}", true);
                ProductImage.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + $"\\Resources\\ItemsImages\\{ArticleNumerTextBox.Text}{fileExtension}", UriKind.Absolute));
                ArticleNumerTextBox.IsReadOnly = true;
            }
            else if (string.IsNullOrWhiteSpace(ArticleNumerTextBox.Text))
                MessageBox.Show("Перед выбором фото, введите артикул.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show("Вы не выбрали фото!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private async void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Вы действительно хотите удалить данный продукт?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var products = _context.Products;
                products.Remove(products.First(p => p.Id == ArticleNumerTextBox.Text));
                await _context.SaveChangesAsync();
            }
        }

        private async void SaveCreateProductButton_Click(object sender, RoutedEventArgs e)
        {
            var products = _context.Products;
            if (_isEdit)
            {
                var messageBoxResult = MessageBox.Show("Сохранить изменения?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    products.Update(products.First(p => p.Id == ArticleNumerTextBox.Text));
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                Product product = new Product
                {

                };
                products.Add(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
