using System.Diagnostics;
using System.Drawing;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Linq;

using GoodsForPets.Models;
using GoodsForPets.Helpers;

namespace GoodsForPets.Views.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private const int BlockingTime = 10;
        // 0 - default, 1 - showing captcha, 2 - block authorization
        private int _loginAttemts = 0;
        private Stopwatch _stopwatch = new Stopwatch();

        public LoginPage()
        {
            InitializeComponent();
            OnPageLoading();
        }

        private void OnPageLoading()
        {
            CreateCaptcha();
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += DispatcherTimerOnTick;
            dispatcherTimer.Start();
        }

        private void CreateCaptcha()
        {
            Captcha captcha = new Captcha();
            Bitmap captchaBitmap = captcha.Create();
            CaptchaImage.Source = Imaging.CreateBitmapSourceFromHBitmap(captchaBitmap.GetHbitmap(),
                IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        private void GetUserFullName(User user)
        {
            Helper.UserFullName.Visibility = Visibility.Visible;
            Helper.UserFullName.Content = $"{user.Surname} {user.Name} {user.Patronymic}";
        }

        private void DispatcherTimerOnTick(object sender, EventArgs e)
        {
            if (_loginAttemts == 2)
            {
                LoginButton.IsEnabled = false;
                ReloadCaptcahButton.IsEnabled = false;
                CreateCaptcha();
                _stopwatch.Start();
                _loginAttemts = 1;
            }
            else if (_stopwatch.Elapsed.TotalSeconds >= BlockingTime)
            {
                LoginButton.IsEnabled = true;
                ReloadCaptcahButton.IsEnabled = true;
                _stopwatch.Reset();
            }
        }

        private void ReloadCaptcahButton_Click(object sender, RoutedEventArgs e)
        {
            CreateCaptcha();
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new GuestPage());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using (Context context = new Context())
            {
                var users = context.Users;
                if (users.Any(u => u.Login == LoginTextBox.Text &&
                u.Password == UserPasswordBox.Password) && _loginAttemts <= 1)
                {
                    if (_loginAttemts >= 1 && InputCaptchaTextBox.Text != Captcha.Text)
                    {
                        MessageBox.Show("Введенная капча не совпадает с картинкой!", "Ошибка!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                        _loginAttemts++;
                        return;
                    }

                    var logedUser = users.FirstOrDefault(u => u.Login == LoginTextBox.Text &&
                        u.Password == UserPasswordBox.Password);
                    switch (logedUser.RoleId)
                    {
                        case 1:
                            NavigationService?.Navigate(new AdministratorPage());
                            GetUserFullName(logedUser);
                            break;
                        case 3:
                            NavigationService?.Navigate(new ManagerPage());
                            GetUserFullName(logedUser);
                            break;
                        case 2:
                            NavigationService?.Navigate(new UserPage());
                            GetUserFullName(logedUser);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Вы ввели не верный логин или пароль!", "Ошибка!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    if (_loginAttemts <= 2)
                        _loginAttemts++;
                    CaptchaRow.Height = GridLength.Auto;
                    InputCaptchaRow.Height = GridLength.Auto;
                }
            }
        }
    }
}
