
using Shopping.Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shopping.Presentation.Views
{
    /// <summary>
    /// Interaction logic for LoginPanel.xaml
    /// </summary>
    public partial class LoginPanel : UserControl
    {
        public LoginPanel()
        {
       
            InitializeComponent();
            DataContext = new UserViewModel();
        }

        //private void OnLoginClick(object sender, RoutedEventArgs e)
        //{

        //    productsview.Visibility = Visibility.Visible;
        //    Loginsection.Visibility = Visibility.Hidden;


        //}

        //private void OnLogOutClick(object sender, RoutedEventArgs e)
        //{
        //    productsview.Visibility = Visibility.Hidden;
        //    Loginsection.Visibility = Visibility.Visible;

        //}

        //private void OnCheckoutClick(object sender, RoutedEventArgs e)
        //{
        //    ordersview.Visibility = Visibility.Visible;
        //    productsview.Visibility = Visibility.Hidden;

        //}

        //private void OnBackClick(object sender, RoutedEventArgs e)
        //{
        //    ordersview.Visibility = Visibility.Hidden;
        //    productsview.Visibility = Visibility.Visible;
        //}
    }
}
