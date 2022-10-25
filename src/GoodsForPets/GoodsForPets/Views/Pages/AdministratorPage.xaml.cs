using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using GoodsForPets.Helpers;
using GoodsForPets.Models;
using GoodsForPets.Views.Controls;
using GoodsForPets.Views.Windows;

using Microsoft.EntityFrameworkCore;

namespace GoodsForPets.Views.Pages
{
    /// <summary>
    /// Interaction logic for AdministratorPage.xaml
    /// </summary>
    public partial class AdministratorPage : Page
    {
        private List<ItemControl> _startProductsList;
        private List<ItemControl> _productsList;
        private Context _context;

        public AdministratorPage()
        {
            InitializeComponent();
            _context = new Context();
            OnPageLoading();
        }

        private async void OnPageLoading()
        {
            Helper.LogoutButton.Visibility = Visibility.Visible;
            Helper.UserFullName.Visibility = Visibility.Visible;
            Helper.UserFullName.Content = "Гость";
            ManufacturerComboBox.ItemsSource = await _context.Manufacturers.ToListAsync();
            (ManufacturerComboBox.ItemsSource as List<Manufacturer>)?.Insert(0, new Manufacturer() { Id = 0, Name = "Все производители" });
            var products = new List<ItemControl>();

            var dbProducts = await _context.ProductsInfo.Join
                (
                    _context.Manufacturers,
                    pi => pi.ManufacturerId,
                    m => m.Id,
                    (pi, m) => new { pi.Id, pi.SupplierId, pi.CategoryId, Manufacturer = m.Name }
                )
                .Join
                (
                    _context.Suppliers,
                    pi => pi.SupplierId,
                    s => s.Id,
                    (pi, s) => new { pi.Id, pi.CategoryId, pi.Manufacturer, Supplier = s.Name }
                )
                .Join
                (
                    _context.Categories,
                    pi => pi.CategoryId,
                    c => c.Id,
                    (pi, c) => new { pi.Id, pi.Manufacturer, pi.Supplier, Category = c.Name }
                )
                .Join
                (
                    _context.Products,
                    pi => pi.Id,
                    p => p.ProductInfoId,
                    (pi, p) => new ProductRecord
                    (
                        p.Id,
                        p.Name,
                        p.Cost,
                        p.MaxDiscountAmount,
                        p.CurrentDiscountAmount,
                        p.QuantityInStock,
                        p.Description, p.Photo, pi.Manufacturer, pi.Supplier, pi.Category
                    )
                ).ToListAsync();

            foreach (var item in dbProducts)
            {
                ItemControl itemControl = new ItemControl(item.QuantityInStock)
                {
                    Product = item,
                    ProductName = item.Name,
                    Description = item.Description,
                    Manufacturer = item.Manufacturer,
                    Price = item.Cost,
                    Photo = item.Photo
                };

                products.Add(itemControl);
            }

            products = products.OrderBy(pr => pr.ProductName).ToList();
            _productsList = products;
            _startProductsList = products;
            ReloadItems(products);
            ManufacturerComboBox.SelectedIndex = 0;
        }

        private void ReloadItems(List<ItemControl> products)
        {
            ItemsStackPanel.Children.Clear();
            foreach (var item in products)
                ItemsStackPanel.Children.Add(item);
        }

        private void SearchTextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchTextBox.Text))
            {
                ReloadItems(_productsList);
                return;
            }

            var searchResult = new List<ItemControl>();
            string inputText = SearchTextBox.Text.ToLower();
            foreach (var item in _productsList)
            {
                if (item.Name.ToLower().Contains(inputText) ||
                    item.Description.ToLower().Contains(inputText) ||
                    item.Manufacturer.ToLower().Contains(inputText) ||
                    item.Price.ToString().ToLower().Contains(inputText) ||
                    item.WarehouseAmount.ToString().ToLower().Contains(inputText))
                {
                    searchResult.Add(item);
                }
            }

            ReloadItems(searchResult);
        }

        private async void ManufacturerComboBoxOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox)?.SelectedIndex == 0)
            {
                if (Desc.IsChecked == true)
                {
                    _productsList = _startProductsList.OrderByDescending(ic => ic.Price).ToList();
                    ReloadItems(_productsList);
                }
                else
                {
                    _productsList = _startProductsList.OrderBy(ic => ic.Price).ToList();
                    ReloadItems(_productsList);
                }
            }
            else
            {
                int selectedItemId = Convert.ToInt32(((sender as ComboBox)?.SelectedItem as Manufacturer)?.Id);
                string mName = (await _context.Manufacturers.Where(pm => pm.Id == selectedItemId).ToListAsync()).FirstOrDefault().Name;
                if (Desc.IsChecked == true)
                {
                    _productsList = _startProductsList.OrderByDescending(ic => ic.Price).Where(ic => ic.Manufacturer.Split(':')[1].Trim() == mName).ToList();
                    ReloadItems(_productsList);
                }
                else
                {
                    _productsList = _startProductsList.OrderBy(ic => ic.Price).Where(ic => ic.Manufacturer.Split(':')[1].Trim() == mName).ToList();
                    ReloadItems(_productsList);
                }
            }
        }

        private void PricesRadioButtonOnChecked(object sender, RoutedEventArgs e)
        {
            if ((sender as RadioButton)?.Name == "Desc")
            {
                _productsList = _productsList.OrderByDescending(ic => ic.Price).ToList();
                ReloadItems(_productsList);
            }
            else
            {
                _productsList = _productsList.OrderBy(ic => ic.Price).ToList();
                ReloadItems(_productsList);
            }
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow(null, false);
            productWindow.ShowDialog();
        }
    }
}
