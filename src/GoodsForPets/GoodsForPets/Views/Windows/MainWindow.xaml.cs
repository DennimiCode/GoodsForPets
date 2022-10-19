using System.Windows;

using GoodsForPets.Helpers;
using GoodsForPets.Views.Pages;

namespace GoodsForPets.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Helper.LogoutButton = LogoutButton;
            Helper.UserFullName = UserFullNameLabel;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LoginPage());
            LogoutButton.Visibility = Visibility.Hidden;
            UserFullNameLabel.Visibility = Visibility.Hidden;
        }
    }
}
