using GoodsForPets.Helpers;
using GoodsForPets.Models;
using GoodsForPets.Views.Controls;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GoodsForPets.Views.Pages
{
    /// <summary>
    /// Interaction logic for GuestPage.xaml
    /// </summary>
    public partial class GuestPage : Page
    {
        private List<ItemControl> _startProductsList;
        private List<ItemControl> _productsList;
        private Context _context;
        public GuestPage()
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
            (ManufacturerComboBox.ItemsSource as List<Manufacturer>).Insert(0, new Manufacturer() { Id = 0, Name = "Все производители" });
            var products = new List<ItemControl>();
            var dbProducts = await _context.Products.Join(_context.ProductsInfo,
                                                                p => p.ProductInfoId,
                                                                pi => pi.Id,
                                                                (p, pi) => new { p.Name, p.Description, p.Cost, p.QuantityInStock, pi.Manufacturer })
                                                            .Join(_context.Manufacturers,
                                                                p => p.Manufacturer,
                                                                pm => pm.Id,
                                                                (p, pm) => new { p.Name, p.Description, p.Cost, p.QuantityInStock, Manufacture = pm.Name }).ToListAsync();
            var dbSubProducts = await _context.SubProducts.Join(_context.ProductsInfo,
                                                                sp => sp.ProductInfoId,
                                                                pi => pi.Id,
                                                                (sp, pi) => new { sp.Name, sp.Description, sp.Cost, sp.QuantityInStock, pi.Manufacturer })
                                                             .Join(_context.Manufacturers,
                                                                p => p.Manufacturer,
                                                                pm => pm.Id,
                                                                (p, pm) => new { p.Name, p.Description, p.Cost, p.QuantityInStock, Manufacture = pm.Name }).ToListAsync();
            foreach (var item in dbProducts)
            {
                ItemControl itemControl = new ItemControl(item.QuantityInStock)
                {
                    ProductName = item.Name,
                    Description = item.Description,
                    Manufacturer = item.Manufacture,
                    Price = item.Cost
                };

                await _context.SaveChangesAsync();
                products.Add(itemControl);
            }

            foreach (var item in dbSubProducts)
            {
                ItemControl itemControl = new ItemControl(item.QuantityInStock)
                {
                    ProductName = item.Name,
                    Description = item.Description,
                    Manufacturer = item.Manufacture,
                    Price = item.Cost
                };

                products.Add(itemControl);
            }

            _productsList = products;
            _startProductsList = products;
            ReloadItems(products);
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
            if ((sender as ComboBox).SelectedIndex == 0)
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
                int selectedItemID = Convert.ToInt32((sender as ComboBox).SelectedValue);
                string mName = (await _context.Manufacturers.Where(pm => pm.Id == selectedItemID).ToListAsync()).FirstOrDefault().Name;
                if (Desc.IsChecked == true)
                {
                    _productsList = _startProductsList.OrderByDescending(ic => ic.Price).Where(ic => ic.Manufacturer == mName).ToList();
                    ReloadItems(_productsList);
                }
                else
                {
                    _productsList = _startProductsList.OrderBy(ic => ic.Price).Where(ic => ic.Manufacturer == mName).ToList();
                    ReloadItems(_productsList);
                }
            }
        }

        private void PricesRadioButtonOnChecked(object sender, RoutedEventArgs e)
        {
            if ((sender as RadioButton).Name == "Desc")
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
    }
}
