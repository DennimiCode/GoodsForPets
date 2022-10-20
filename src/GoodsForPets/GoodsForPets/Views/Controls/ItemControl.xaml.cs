using GoodsForPets.Views.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GoodsForPets.Views.Controls
{
    /// <summary>
    /// Логика взаимодействия для ItemControl.xaml
    /// </summary>
    public partial class ItemControl : UserControl
    {
        public string ProductName
        {
            get => ProductNameTextBlock.Text;
            set => ProductNameTextBlock.Text += value;
        }
        public string Description
        {
            get => DescriptionTextBlock.Text;
            set => DescriptionTextBlock.Text += value;
        }
        public string Manufacturer
        {
            get => ManufactureTextBlock.Text;
            set => ManufactureTextBlock.Text += value;
        }
        public double Price
        {
            get => Convert.ToDouble(PriceTextBlock.Text.Split(':')[1].Trim());
            set => PriceTextBlock.Text += value.ToString();
        }

        public string Photo
        {
            get => ICImage.Source.ToString();
            set => ICImage.Source = new BitmapImage(new Uri("/Resources/ItemsImages/" + (value != null ? value : "emptyItem.png"), UriKind.Relative));
        }

        public int WarehouseAmount { get; set; }

        public ItemControl(int warehouseAmount)
        {
            InitializeComponent();

            WarehouseAmountTextBlock.Text += warehouseAmount.ToString();
            WarehouseAmount = warehouseAmount;

            if (warehouseAmount == 0)
                MainGrid.Background = Brushes.Gray;
        }

        private void MainGrid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
