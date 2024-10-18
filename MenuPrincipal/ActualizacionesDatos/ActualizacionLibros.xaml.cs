using MenuPrincipal.BD.Models;
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
using System.Windows.Shapes;


using System.Data.SqlClient;
using System.Data;
using System.Collections;


namespace MenuPrincipal.ActualizacionesDatos
{
    /// <summary>
    /// Lógica de interacción para ActualizacionLibros.xaml
    /// </summary>
    public partial class ActualizacionLibros : Window
    {
        private DetallesLibros Libros;
        public ActualizacionLibros(DetallesLibros Libros)
        {
            InitializeComponent();
            this.Libros = Libros;

            CargarDatos();
        }


        public void CargarDatos()
        {
            CargarImgDes();
            EditTituloTextBox.Text = Libros.Titulo;
            EditAutorTextBox.Text = Libros.Autor;
            EditEditorialTextBox.Text = Libros.Editorial;
            EditCategoriaTextBox.Text = Libros.Categoria;
            EditEdicionTextBox.Text = Libros.Edicion;

            

            // Mostrar el panel de edición
        }

        public List<object> ObtenerImgDescripcion(string edicion)
        {
            // Lista para almacenar la descripción y la imagen
            List<object> listaDatos = new List<object>();

            try
            {
                using (var conDb = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conDb.Open();

                    using (var cmd = conDb.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "select Imagen, Descripcion from Ediciones where ISBN = @edicion";

                        // Usamos un parámetro para evitar SQL Injection
                        cmd.Parameters.AddWithValue("@edicion", edicion);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                byte[] imagen = reader["Imagen"] as byte[];

                                // Obtiene la descripción
                                string descripcion = reader["Descripcion"].ToString();

                                listaDatos.Add(imagen);
                                listaDatos.Add(descripcion);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error inesperado: " + e.Message, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return listaDatos;  // Devolver la lista con la imagen y la descripción
        }

        private void CargarImgDes()
        {
            // Llamada al método para obtener la imagen y la descripción
            List<object> ListImgDes = ObtenerImgDescripcion(Libros.Edicion);

            // Asegurarse de que haya datos en la lista
            if (ListImgDes.Count >= 2)
            {
                // Asignar la descripción al TextBox
                EditDescripcionTextBox.Text = ListImgDes[1].ToString();

                // Manejar la imagen
                byte[] imagenBytes = ListImgDes[0] as byte[];
                if (imagenBytes != null)
                {
                    // Convertir los bytes a BitmapImage usando el método
                    BitmapImage imagen = ConvertirABitmapImage(imagenBytes);

                    // Crear un ViewModel temporal para establecer la imagen
                    var viewModel = new { ImageData = imagen };

                    // Asignar el DataContext de la ventana o del control que contiene la imagen
                    this.DataContext = viewModel;
                }
                else
                {
                    MessageBox.Show("No se encontró una imagen para esta edición.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("No se encontraron datos para esta edición.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
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
