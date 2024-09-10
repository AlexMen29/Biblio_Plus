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
        public PgLibros()
        {
            InitializeComponent();
            CargarDatos();
        }


        private string connectionString = "Data Source=DESKTOP-03IRMT5\\SQLEXPRESS;Initial Catalog=baseAlex;Integrated Security=True;TrustServerCertificate=True;";

        private void CargarDatos()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM prueba";
                    SqlCommand command = new SqlCommand(query, connection);

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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar a la base de datos: {ex.Message}");
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
