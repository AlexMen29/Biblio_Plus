using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace MenuPrincipal.FrmMenu
{
    /// <summary>
    /// Lógica de interacción para PgLibros.xaml
    /// </summary>
    public partial class PgLibros : Page
    {
        private DataTable dataTable;
        public PgLibros()
        {
            InitializeComponent();
            CargarDatos();
            ConfigurarSlider();
        }


        SqlConnection conDB = new SqlConnection(MenuPrincipal.Properties.Settings.Default.conexionDB);

        private void CargarDatos()
        {

            try
            {
                conDB.Open();
                string query = "SELECT * FROM prueba";
                SqlCommand command = new SqlCommand(query, conDB);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                // Crear una nueva columna para las imágenes
                dataTable.Columns.Add("ImageData", typeof(BitmapImage));

                foreach (DataRow row in dataTable.Rows)
                {
                    if (row["imagen"] != DBNull.Value)
                    {
                        byte[] imageBytes = (byte[])row["imagen"];
                        BitmapImage bitmapImage = ConvertirABitmapImage(imageBytes);
                        row["ImageData"] = bitmapImage;
                    }
                }

                // Asignar los datos al ItemsControl
                contentGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar a la base de datos: {ex.Message}");
            }
            finally { 
                conDB.Close();
            }
        }
        private void ConfigurarSlider()
        {
            if (dataTable != null)
            {
                // Configurar el slider según la cantidad de imágenes cargadas
                ImageSlider.Maximum = dataTable.Rows.Count - 1;
            }
        }

        // Evento que cambia la imagen mostrada en el slider
        private void ImageSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (dataTable != null)
            {
                int index = (int)ImageSlider.Value;
                DataRow row = dataTable.Rows[index];

                // Mostrar la imagen seleccionada en el ImageDisplay
                ImageDisplay.Source = (BitmapImage)row["ImageData"];
            }
        }

        // Método para convertir un array de bytes a BitmapImage
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
