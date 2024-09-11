using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using MenuPrincipal.PageReport;
using Microsoft.Data.SqlClient;
using MenuPrincipal.FrmMenu;

namespace MenuPrincipal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool Maximizado=false;
        private void Border_MouseLeftButtondDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (Maximizado)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    Maximizado = false;

                }

                else
                {
                    this.WindowState = WindowState.Maximized;
                 

                    Maximizado = true;

                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PgLibros Page1= new PgLibros();
            frContenido.NavigationService.Navigate(Page1);

            ImgLogo.Visibility=Visibility.Hidden;

          
        }

        private void btnreport_Click(object sender, RoutedEventArgs e)
        {
            Report page2 = new Report();
            frContenido.NavigationService.Navigate(page2);
            ImgLogo.Visibility=Visibility.Hidden;
        }
    }
}