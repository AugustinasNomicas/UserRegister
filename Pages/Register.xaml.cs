using System.Windows;

namespace UserRegister.Pages
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class Register
    {
        public Register()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (NavigationService != null) NavigationService.Navigate(new LanguageSelect());
        }
    }
}
