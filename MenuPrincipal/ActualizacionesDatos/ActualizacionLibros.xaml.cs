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

using Microsoft.Win32; // Para OpenFileDialog
using System.IO;       // Para manejar streams
using System.Windows.Media.Imaging; // Para BitmapImage
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MenuPrincipal.DatosGenerales;
using System.Data.Common;
using MenuPrincipal.BD.Services;


namespace MenuPrincipal.ActualizacionesDatos
{
    /// <summary>
    /// Lógica de interacción para ActualizacionLibros.xaml
    /// </summary>
    public partial class ActualizacionLibros : Window
    {
        private DetallesLibros Libros;
        DatosGlobales datos = new DatosGlobales();
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
            EditEdicionTextBox.Text = Libros.Edicion;
           

            LlenarCajas(datos.consultaAutor, EditAutorComboBox, "NombreAutor",Libros.Autor);
            LlenarCajas(datos.consultaEdiorial, EditEditorialComboBox, "NombreEditorial",Libros.Editorial);
            LlenarCajas(datos.consultaCategoria, EditCategoriaComboBox, "NombreCategoria",Libros.Categoria);



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
                    BitmapImage imagen = datos.ConvertirABitmapImage(imagenBytes);

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



        //Cargar datos para modificacion de Autor, Editorial y categoria

        public void LlenarCajas(string consulta, ComboBox elementoBox, string columna, string valorPorDefecto)
        {
            try
            {
                // Lista con valores correspondientes a ComboBox
                List<string> Lista = new List<string>();
                using (var conn = new SqlConnection(Properties.Settings.Default.conexionDB))
                {
                    conn.Open();

                    using (var command = new SqlCommand(consulta, conn))
                    {
                        using (DbDataReader dr = command.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Lista.Add(dr[columna].ToString());
                            }
                        }
                    }
                }

                // Asigna la lista de valores al ComboBox
                elementoBox.ItemsSource = Lista;

                // Establece el valor por defecto que ya está registrado en la base de datos
                if (!string.IsNullOrEmpty(valorPorDefecto) && Lista.Contains(valorPorDefecto))
                {
                    elementoBox.SelectedItem = valorPorDefecto;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show($"Error inesperado: {e.Message}");
            }
        }

        private void btnCargarImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            // Mostrar el explorador de archivos
            if (openFileDialog.ShowDialog() == true)
            {
                // Obtener la ruta de la imagen seleccionada
                string rutaImagen = openFileDialog.FileName;

                byte[] imageBytes = File.ReadAllBytes(rutaImagen);

                // Reutilizar el método de la clase DatosGlobales para convertir los bytes a BitmapImage
                BitmapImage imagen = datos.ConvertirABitmapImage(imageBytes);

                // Asignar la imagen al control Image
                ImagePreview.Source = imagen;
            }
        }

        private void bntModificar_Click(object sender, RoutedEventArgs e)
        {

            ObtenerId();


        }


        private ArrayList ObtenerId()
        {
            ArrayList id = new ArrayList();
            id = MetodosDetallesLibros.ObtenerIdModLibros(EditAutorComboBox.SelectedItem.ToString(), EditEditorialComboBox.SelectedItem.ToString(), EditCategoriaComboBox.SelectedItem.ToString());

            if (id.Count > 0)
            {
                // Usar String.Join para concatenar los elementos de la lista en una cadena
                string valores = string.Join(", ", id.Cast<object>().Select(i => i.ToString()));

                MessageBox.Show("Valores: " + valores);
            }
            else
            {
                MessageBox.Show("No se encontraron valores.");
            }


            return id;
            
        }
    }
}
