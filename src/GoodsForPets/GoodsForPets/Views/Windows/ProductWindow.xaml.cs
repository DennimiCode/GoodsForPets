using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using GoodsForPets.Models;

using Microsoft.EntityFrameworkCore;
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
        private string _newProductPhoto;
        public ProductWindow(ProductRecord product, bool isEdit = true)
        {
            InitializeComponent();
            _product = product;
            _isEdit = isEdit;
            _context = new Context();
        }

        private async void ProductWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CategoryComboBox.ItemsSource = await _context.Categories.ToListAsync();
            ManufacturerComboBox.ItemsSource = await _context.Manufacturers.ToListAsync();
            SupplierComboBox.ItemsSource = await _context.Suppliers.ToListAsync();

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
                    ReloadComboBoxes();
                }
            }
            else
            {
                Title = "Добавление товара";
                SaveCreateProductButton.Content = "Добавить";
                DeleteProductButton.Visibility = Visibility.Hidden;
                ReloadComboBoxes();
            }
        }

        private async void ReloadComboBoxes()
        {
            CategoryComboBox.ItemsSource = await _context.Categories.ToListAsync();
            ManufacturerComboBox.ItemsSource = await _context.Manufacturers.ToListAsync();
            SupplierComboBox.ItemsSource = await _context.Suppliers.ToListAsync();

            if (_isEdit)
            {
                SupplierComboBox.SelectedItem = _context.Suppliers.First(m => m.Name == _product.Supplier);
                ManufacturerComboBox.SelectedItem = _context.Manufacturers.First(m => m.Name == _product.Manufacturer);
                CategoryComboBox.SelectedItem = _context.Categories.First(c => c.Name == _product.Category);
            }
            else
            {
                (SupplierComboBox.ItemsSource as List<Supplier>)?.Insert(0, new Supplier { Id = 0, Name = "Не выбран" });
                (ManufacturerComboBox.ItemsSource as List<Manufacturer>)?.Insert(0, new Manufacturer { Id = 0, Name = "Не выбран" });
                (CategoryComboBox.ItemsSource as List<Category>)?.Insert(0, new Category { Id = 0, Name = "Не выбрана", Unit = "Нет" });
                SupplierComboBox.SelectedIndex = 0;
                ManufacturerComboBox.SelectedIndex = 0;
                CategoryComboBox.SelectedIndex = 0;
            }
        }

        private void AddSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            AddSupplierWindow addSupplierWindow = new AddSupplierWindow();
            addSupplierWindow.ShowDialog();
            ReloadComboBoxes();
        }

        private void AddManufacturerButton_Click(object sender, RoutedEventArgs e)
        {
            AddManufacturerWindow addManufacturerWindow = new AddManufacturerWindow();
            addManufacturerWindow.ShowDialog();
            ReloadComboBoxes();
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryWindow addCategoryWindow = new AddCategoryWindow();
            addCategoryWindow.ShowDialog();
            ReloadComboBoxes();
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
                _newProductPhoto = string.IsNullOrWhiteSpace($"{ArticleNumerTextBox.Text}{fileExtension}") ? null : $"{ArticleNumerTextBox.Text}{fileExtension}";
            }
            else if (string.IsNullOrWhiteSpace(ArticleNumerTextBox.Text))
                MessageBox.Show("Перед выбором фото, введите артикул.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show("Вы не выбрали фото!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private async void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Вы действительно хотите удалить данный товар?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var ordersProducts = _context.OrderProducts;
                if (!ordersProducts.Any(op => op.ProductId == ArticleNumerTextBox.Text))
                {
                    var products = _context.Products;
                    products.Remove(products.First(p => p.Id == ArticleNumerTextBox.Text));
                    await _context.SaveChangesAsync();
                    MessageBox.Show("Товар успешно удален!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Question);
                    Close();
                }
                else
                {
                    MessageBox.Show("Данный продукт находится в заказе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void SaveCreateProductButton_Click(object sender, RoutedEventArgs e)
        {
            var products = _context.Products;
            if (SupplierComboBox.SelectedIndex != 0
                    && ManufacturerComboBox.SelectedIndex != 0
                    && CategoryComboBox.SelectedIndex != 0
                    && !string.IsNullOrWhiteSpace(ArticleNumerTextBox.Text)
                    && !string.IsNullOrWhiteSpace(NameTextBox.Text)
                    && !string.IsNullOrWhiteSpace(CostTextBox.Text)
                    && !string.IsNullOrWhiteSpace(MaxDiscountAmountTextBox.Text)
                    && !string.IsNullOrWhiteSpace(CurrentDiscountAmountTextBox.Text)
                    && !string.IsNullOrWhiteSpace(QuantityInStockTextBox.Text)
                    && !string.IsNullOrWhiteSpace(DescriptionTextBlock.Text)
                    && double.TryParse(CostTextBox.Text, out double cost)
                    && int.TryParse(MaxDiscountAmountTextBox.Text, out int maxDiscount)
                    && int.TryParse(CurrentDiscountAmountTextBox.Text, out int currentDiscount)
                    && int.TryParse(QuantityInStockTextBox.Text, out int quantityInStock))
            {
                if (_isEdit)
                {
                    var messageBoxResult = MessageBox.Show("Сохранить изменения?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        var product = products.Where(p => p.Id == ArticleNumerTextBox.Text).FirstOrDefault();
                        product.Name = NameTextBox.Text;
                        product.Cost = cost;
                        product.MaxDiscountAmount = maxDiscount;
                        product.CurrentDiscountAmount = currentDiscount;
                        product.QuantityInStock = quantityInStock;
                        product.Description = DescriptionTextBlock.Text;
                        product.Photo = _newProductPhoto;
                        await _context.SaveChangesAsync();
                        MessageBox.Show("Товар успешно обновлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Question);
                        Close();
                    }
                }
                else
                {
                    ProductInfo productInfo = new ProductInfo
                    {
                        CategoryId = _context.Categories.First(c => c.Name == CategoryComboBox.Text).Id,
                        ManufacturerId = _context.Manufacturers.First(m => m.Name == ManufacturerComboBox.Text).Id,
                        SupplierId = _context.Suppliers.First(s => s.Name == SupplierComboBox.Text).Id
                    };
                    _context.ProductsInfo.Add(productInfo);
                    await _context.SaveChangesAsync();

                    Product product = new Product
                    {
                        Id = ArticleNumerTextBox.Text,
                        Name = NameTextBox.Text,
                        Cost = cost,
                        MaxDiscountAmount = maxDiscount,
                        CurrentDiscountAmount = currentDiscount,
                        QuantityInStock = quantityInStock,
                        Description = DescriptionTextBlock.Text,
                        ProductInfoId = productInfo.Id
                    };
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                    MessageBox.Show("Товар успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Question);
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Проверьте корректность введенных данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetDefaultImageButton_Click(object sender, RoutedEventArgs e)
        {
            _newProductPhoto = null;
            ProductImage.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + $"\\Resources\\ItemsImages\\emptyItem.png", UriKind.Absolute));
        }
    }
}
