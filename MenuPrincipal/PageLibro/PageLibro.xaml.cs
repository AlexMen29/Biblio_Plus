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
using Microsoft.Data.SqlClient;
using System.Data;

namespace MenuPrincipal.PageLibro
{
    /// <summary>
    /// Interaction logic for PageLibro.xaml
    /// </summary>
    public partial class PageLibro : Page
    {
        public PageLibro()
        {
            InitializeComponent();
        }

        //Variables
        SqlConnection conDB = new SqlConnection(MenuPrincipal.Properties.Settings.Default.conexionDB);
        string consultaSQL = null;

        private void CargarElementos() {
            CargarTitulo();
            CargarSinopsis();
        }
        private void CargarTitulo() {

            try
            { 
                conDB.Open();
                consultaSQL = "SELECT TITULO FROM LIBROS";
                SqlDataAdapter da = new SqlDataAdapter(consultaSQL, conDB);

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    // Asignamos el valor del primer registro al TextBox
                    txbTitulo.DataContext = dt.Rows[0]["TITULO"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private void CargarSinopsis()
        {

            try
            {
                conDB.Open();
                consultaSQL = "SELECT SINOPSIS FROM LIBROS";
                SqlDataAdapter da = new SqlDataAdapter(consultaSQL, conDB);

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    // Asignamos el valor del primer registro al TextBox
                    txbTitulo.DataContext = dt.Rows[0]["SINOPSIS"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private BitmapImage ConvertirABitmapImage(byte[] imageBytes)
        {
            using (var ms = new System.IO.MemoryStream(imageBytes))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
}
