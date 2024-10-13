using MenuPrincipal.BD.Models;
using MenuPrincipal.BD.Services;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
            CargarTodosLosElementos();
            CargarDatos();


            LlenarBoxFiltros("SELECT DISTINCT A.NombreAutor FROM Libros L JOIN Autores A ON L.AutorID = A.AutorID", AutorComboBox,"NombreAutor");
            LlenarBoxFiltros("SELECT DISTINCT A.NombreEditorial FROM Libros L JOIN Editoriales A ON L.EdicionID = A.EditorialID",EditorialComboBox, "NombreEditorial");
            LlenarBoxFiltros("SELECT DISTINCT A.NombreCategoria FROM Libros L JOIN Categorias A ON L.CategoriaID = A.CategoriaID", CategoriaComboBox,"NombreCategoria");
            LlenarBoxFiltros("SELECT DISTINCT A.ISBN FROM Libros L JOIN Ediciones A ON L.EdicionID = A.EdicionID",EdicionComboBox,"ISBN");
        }

        public List<DetallesLibros> ListaDataGrid;

        SqlConnection conDB = new SqlConnection(MenuPrincipal.Properties.Settings.Default.conexionDB);


        //Funcion creada para llamar solo una vez para cargar todos los elementos
        private void CargarTodosLosElementos()
        {
            CargarElementosPorGenero("SELECT TOP 3* FROM LIBROS ORDER BY LibroID DESC", contentGrid);
            CargarElementosPorGenero("SELECT *FROM LIBROS WHERE EditorialID=2", contenGridDrama);
        }

        //funcion que carga elementos segun genero
        private void CargarElementosPorGenero(string query,ItemsControl contenGrid)
        {
            try
            {
                conDB.Open();           
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
                contenGrid.ItemsSource = dataTable.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar a la base de datos: {ex.Message}");
            }
            finally
            {
                conDB.Close();
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






        #region ADMINISTRADOR

        public void CargarDatos()
        {
            ListaDataGrid = MetodosDetallesLibros.MostrarLibros();
            LibrosDataGrid.ItemsSource = ListaDataGrid;

        }




        public void LlenarBoxFiltros(string consulta, ComboBox elementoBox, string columna)
        {
            //Lista con valores correspondientes a ComboBox
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
                        Lista.Add("Ninguno");
                    }
                }
            }

            elementoBox.ItemsSource = Lista; // Asigna la lista al ComboBox
        }




        //public void AplicarFiltro(string seleccion, string propiedad, DataGrid dataGrid)
        //{
        //    if (!string.IsNullOrEmpty(seleccion) && seleccion != "Ninguno")
        //    {
        //        // Filtramos la lista de libros según la propiedad (Autor, Editorial, etc.)
        //        List<DetallesLibros> librosFiltrados = ListaDataGrid //MetodosDetallesLibros.MostrarLibros()
        //            .Where(libro =>
        //            {
        //                // Obtenemos el valor de la propiedad dinámica (Autor, Editorial, etc.)
        //                var valorPropiedad = typeof(DetallesLibros).GetProperty(propiedad).GetValue(libro).ToString();
        //                return valorPropiedad.Equals(seleccion, StringComparison.OrdinalIgnoreCase);
        //            })
        //            .ToList();

        //        // Asignamos los libros filtrados al DataGrid
        //        dataGrid.ItemsSource = librosFiltrados;
        //        ListaDataGrid = librosFiltrados;

        //    }
        //    else
        //    {
        //        // Si no hay selección, cargamos todos los datos
        //        CargarDatos();
        //    }
        //}

        public void AplicarFiltro()
        {
            // Obtenemos la lista completa de libros
            List<DetallesLibros> librosFiltrados = MetodosDetallesLibros.MostrarLibros();

            // Filtramos por autor si hay un valor seleccionado
            if (AutorComboBox.SelectedItem != null && AutorComboBox.SelectedItem.ToString() != "Ninguno")
            {
                string autorSeleccionado = AutorComboBox.SelectedItem.ToString();
                librosFiltrados = librosFiltrados
                    .Where(libro => libro.Autor.Equals(autorSeleccionado, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Filtramos por editorial si hay un valor seleccionado
            if (EditorialComboBox.SelectedItem != null && EditorialComboBox.SelectedItem.ToString() != "Ninguno")
            {
                string editorialSeleccionada = EditorialComboBox.SelectedItem.ToString();
                librosFiltrados = librosFiltrados
                    .Where(libro => libro.Editorial.Equals(editorialSeleccionada, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Filtramos por categoría si hay un valor seleccionado
            if (CategoriaComboBox.SelectedItem != null && CategoriaComboBox.SelectedItem.ToString() != "Ninguno")
            {
                string categoriaSeleccionada = CategoriaComboBox.SelectedItem.ToString();
                librosFiltrados = librosFiltrados
                    .Where(libro => libro.Categoria.Equals(categoriaSeleccionada, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Filtramos por edición si hay un valor seleccionado
            if (EdicionComboBox.SelectedItem != null && EdicionComboBox.SelectedItem.ToString() != "Ninguno")
            {
                string edicionSeleccionada = EdicionComboBox.SelectedItem.ToString();
                librosFiltrados = librosFiltrados
                    .Where(libro => libro.Edicion.Equals(edicionSeleccionada, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Asignamos los libros filtrados al DataGrid
            LibrosDataGrid.ItemsSource = librosFiltrados;

          
        }



        private void AutorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           
            AplicarFiltro();
        }
        private void EditorialComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AplicarFiltro();
        }

        private void CategoriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AplicarFiltro();
        }

        private void EdicionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AplicarFiltro();
        }


        #endregion

        private void btnQuitarFiltros_Click(object sender, RoutedEventArgs e)
        {
            LimpiarFiltros();
        }
        public void LimpiarFiltros()
        {
            AutorComboBox.SelectedIndex = -1;
            EditorialComboBox.SelectedIndex = -1;
            CategoriaComboBox.SelectedIndex = -1;
            EdicionComboBox.SelectedIndex = -1;
        }
    }






}
